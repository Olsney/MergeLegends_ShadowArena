using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "PlayerUnitData", menuName = "StaticData/Unit")]
    public class UnitStaticData : ScriptableObject
    {
        public UnitTypeId UnitTypeId;
        public List<UnitLvlEntrySetup> UnitLvlStatsEntries = new();
        public GeneralData GeneralData;
    
        [HideInInspector]
        public UnitLvlStatsData GetStatsLvl(UnitLvlTypeId lvlTypeId)
        {
            foreach (UnitLvlEntrySetup entry in UnitLvlStatsEntries)
            {
                if (entry.LvlTypeId == lvlTypeId)
                    return entry.UnitLvlStats;
            }
            return null;
        }
    }
}