using System;
using System.Collections.Generic;
using Code.StaticData;
using UnityEngine;

namespace Code.Services.UnitStatsAutoGeneratorServices
{
    public class UnitStatsAutoGeneratorService : IUnitStatsAutoGeneratorService
    {
        private readonly Dictionary<(UnitStaticData, int), UnitLevelStatsData> _generatedStats = new();

        public UnitLevelStatsData GetGeneratedStats(UnitStaticData unitData, int levelIndex)
        {
            List<UnitStatsByLevelIndexSetup> setups = unitData.UnitStatsByLevelIndexSetups;
            int lastLevelIndex = setups.Count - 1;

            if (levelIndex <= lastLevelIndex)
                throw new ArgumentOutOfRangeException(nameof(levelIndex),
                    $"Level {levelIndex} is handmade in {unitData.name}, use IUnitStatsService");

            (UnitStaticData, int) key = (unitData, levelIndex);

            if (_generatedStats.TryGetValue(key, out UnitLevelStatsData cachedStats))
                return cachedStats;

            UnitLevelStatsData lastStats = setups[lastLevelIndex].UnitLevelStatsData;
            UnitStatsGenerationSettings settings = unitData.Generation;
            int levelsAhead = levelIndex - lastLevelIndex;

            var stats = new UnitLevelStatsData
            {
                Hp = Round(lastStats.Hp * Mathf.Pow(settings.HpGrowth, levelsAhead), settings.SignificantDigits),
                Damage = Round(lastStats.Damage * Mathf.Pow(settings.DamageGrowth, levelsAhead),
                    settings.SignificantDigits),
                ViewPrefab = lastStats.ViewPrefab
            };

            _generatedStats.Add(key, stats);
            return stats;
        }

        private float Round(float value, int significantDigits)
        {
            if (significantDigits <= 0 || value <= 0f)
                return value;

            int digits = Mathf.FloorToInt(Mathf.Log10(value)) + 1;
            float scale = Mathf.Pow(10f, Mathf.Max(0, digits - significantDigits));
            return Mathf.Round(value / scale) * scale;
        }
    }
}