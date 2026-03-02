using Code.Units;
using Code.Units.PlayerUnit.Config;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "PlayerUnitData", menuName = "StaticData/PlayerUnit")]
    public class PlayerUnitStaticData : ScriptableObject
    {
        public PlayerUnitTypeId PlayerUnitTypeId;
        public PlayerUnitConfig PlayerUnitConfig;
        public TargetFinderConfig TargetFinderConfig;
        public AttackConfig AttackConfig;
    }
}