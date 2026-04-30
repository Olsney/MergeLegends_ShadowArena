using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "PlayerUnitData", menuName = "StaticData/Unit")]
    public class UnitStaticData : ScriptableObject
    {
        public UnitAttackTypeId unitAttackTypeId;
        public List<UnitStatsByLevelIndexSetup> UnitStatsByLevelIndexSetups = new();
        public GeneralData GeneralData;

        [HideInInspector]
        public UnitLevelStatsData GetStatsLvl(int unitCurrentLevelIndex)
        {
            if (UnitStatsByLevelIndexSetups == null || UnitStatsByLevelIndexSetups.Count == 0 ||
                unitCurrentLevelIndex < 0)
                throw new InvalidOperationException($"UnitStatsByLevelIndexSetups is empty for {name}");

            if (unitCurrentLevelIndex >= UnitStatsByLevelIndexSetups.Count)
                return null;

            return UnitStatsByLevelIndexSetups[unitCurrentLevelIndex].UnitLevelStatsData;
        }
    }
}