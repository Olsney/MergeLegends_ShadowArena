using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class UnitStatsGenerationSettings
    {
        public bool AutoGenerate = true;

        [Min(0)] public float BaseHp;
        [Min(0)] public float BaseDamage;

        [Min(1)] public float HpGrowth;
        [Min(1)] public float DamageGrowth;

        [Tooltip("Значащих цифр. 0 = без округления")] [Range(0, 4)]
        public int SignificantDigits;

        public void Apply(List<UnitStatsByLevelIndexSetup> setups)
        {
            for (int i = 0; i < setups.Count; i++)
            {
                UnitStatsByLevelIndexSetup setup = setups[i];

                if (setup.Override)
                    continue;

                setup.UnitLevelStatsData ??= new UnitLevelStatsData();
                setup.UnitLevelStatsData.Hp = Round(BaseHp * Mathf.Pow(HpGrowth, i));
                setup.UnitLevelStatsData.Damage = Round(BaseDamage * Mathf.Pow(DamageGrowth, i));
            }
        }

        private float Round(float value)
        {
            if (SignificantDigits <= 0 || value <= 0f)
                return value;

            int digits = Mathf.FloorToInt(Mathf.Log10(value)) + 1;
            float scale = Mathf.Pow(10f, Mathf.Max(0, digits - SignificantDigits));
            return Mathf.Round(value / scale) * scale;
        }
    }
}