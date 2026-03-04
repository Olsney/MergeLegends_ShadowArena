using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "PlayerUnitData", menuName = "StaticData/Unit")]
    public class UnitStaticData : ScriptableObject
    {
        public UnitTypeId UnitTypeId;
        public List<UnitLvlEntry> UnitLvlStatsConfigs = new();
        public GeneralConfig GeneralConfig;
    
        [HideInInspector]
        public UnitLvlStatsConfig GetStatsLvl(UnitLvlTypeId lvlTypeId)
        {
            foreach (UnitLvlEntry entry in UnitLvlStatsConfigs)
            {
                if (entry.LvlTypeId == lvlTypeId)
                    return entry.StatsConfig;
            }
            return null;
        }
    }
}