using Code.Units;
using Code.Units.Config;
using Code.Units.PlayerUnit.Config;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "PlayerUnitData", menuName = "StaticData/MeleeUnit")]
    public class MeleeUnitStaticData : ScriptableObject
    {
        public UnitLvlTypeId UnitLvlTypeId;
        public UnitConfig UnitConfig;
        public TargetConfig TargetConfig;
    }
}