using System;
using System.Collections.Generic;
using Code.Services.UnitStatsAutoGeneratorServices;
using Code.StaticData;

namespace Code.Services.UnitStatsServices
{
    public class UnitStatsService : IUnitStatsService
    {
        private readonly IUnitStatsAutoGeneratorService _autoGenerator;

        public UnitStatsService(IUnitStatsAutoGeneratorService autoGenerator) =>
            _autoGenerator = autoGenerator;

        public UnitLevelStatsData GetStats(UnitStaticData unitStaticData, int levelIndex)
        {
            if (unitStaticData == null)
                throw new ArgumentNullException(nameof(unitStaticData));

            List<UnitStatsByLevelIndexSetup> setups = unitStaticData.UnitStatsByLevelIndexSetups;

            if (setups == null || setups.Count == 0)
                throw new InvalidOperationException($"UnitStatsByLevelIndexSetups is empty for {unitStaticData.name}");

            if (levelIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(levelIndex), $"Level {levelIndex} is negative");

            return levelIndex < setups.Count
                ? setups[levelIndex].UnitLevelStatsData
                : _autoGenerator.GetGeneratedStats(unitStaticData, levelIndex);
        }
    }
}