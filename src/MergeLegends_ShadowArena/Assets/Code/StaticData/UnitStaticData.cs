using System.Collections.Generic;
using Code.StaticData;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerUnitData", menuName = "StaticData/Unit")]
public class UnitStaticData : ScriptableObject
{
    public UnitTypeId UnitTypeId;
    public List<UnitLvlEntry> UnitLvlStatsConfigs = new();
    public GeneralConfig GeneralConfig;
    
    [HideInInspector]
    public UnitLvlStatsConfig GetStatsForLvl(UnitLvlTypeId lvlTypeId)
    {
        foreach (UnitLvlEntry entry in UnitLvlStatsConfigs)
        {
            if (entry.LvlTypeId == lvlTypeId)
                return entry.StatsConfig;
        }
        return null;
    }
}