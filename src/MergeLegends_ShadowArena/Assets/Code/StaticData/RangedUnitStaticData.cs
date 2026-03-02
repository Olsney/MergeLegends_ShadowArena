using Code.StaticData;
using Code.Units.Config;
using Code.Units.PlayerUnit.Config;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerUnitData", menuName = "StaticData/RangedUnit")]
public class RangedUnitStaticData : ScriptableObject
{
    public UnitLvlTypeId UnitLvlTypeId;
    public UnitConfig UnitConfig;
    public TargetConfig TargetConfig;
    public GameObject MagicSpherePrefab;
}