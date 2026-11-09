using System;
using System.Collections.Generic;
using Code.StaticData;
using UnityEngine;

namespace Code.Services.UnitStatsAutoGeneratorServices
{
    public class UnitStatsAutoGeneratorService : IUnitStatsAutoGeneratorService
    {
        public UnitLevelStatsData GetGeneratedStats(UnitStaticData unitData, int levelIndex)
        {
            List<UnitStatsByLevelIndexSetup> setups = unitData.UnitStatsByLevelIndexSetups;
            int lastLevelIndex = setups.Count - 1;

            if (levelIndex <= lastLevelIndex)
                throw new ArgumentOutOfRangeException(nameof(levelIndex),
                    $"Level {levelIndex} is handmade in {unitData.name}, use IUnitStatsService");

            UnitLevelStatsData lastStats = setups[lastLevelIndex].UnitLevelStatsData;
            UnitStatsGenerationSettings settings = unitData.Generation;
            int levelsAhead = levelIndex - lastLevelIndex;

            return new UnitLevelStatsData
            {
                Hp = Round(lastStats.Hp * Mathf.Pow(settings.HpGrowth, levelsAhead), settings.SignificantDigits),
                Damage = Round(lastStats.Damage * Mathf.Pow(settings.DamageGrowth, levelsAhead),
                    settings.SignificantDigits),
                ViewPrefab = lastStats.ViewPrefab
            };
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