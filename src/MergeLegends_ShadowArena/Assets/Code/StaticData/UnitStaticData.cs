using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "PlayerUnitData", menuName = "StaticData/Unit")]
    public class UnitStaticData : ScriptableObject
    {
        public UnitTypeId UnitTypeId;
        public List<UnitStatsByLvlSetup> unitStatsByLvlSetups = new();
        public GeneralData GeneralData;
    
        [HideInInspector]
        public UnitLvlStatsData GetStatsLvl(UnitLvlTypeId lvlTypeId)
        {
            foreach (UnitStatsByLvlSetup entry in unitStatsByLvlSetups)
            {
                if (entry.LvlTypeId == lvlTypeId)
                    return entry.UnitLvlStats;
            }
            return null;
        }
    }
}