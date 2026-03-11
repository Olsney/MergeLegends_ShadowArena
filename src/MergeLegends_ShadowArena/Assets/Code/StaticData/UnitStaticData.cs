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
            => UnitStatsByLevelIndexSetups[unitCurrentLevelIndex].UnitLevelStatsData;
    }
}