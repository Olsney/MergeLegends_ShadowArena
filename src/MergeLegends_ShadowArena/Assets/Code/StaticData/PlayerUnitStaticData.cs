using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "PlayerUnitData", menuName = "StaticData/PlayerUnit")]
    public class PlayerUnitStaticData : ScriptableObject
    {
        public PlayerUnitTypeId playerUnitTypeId;
        public PlayerUnitConfig playerUnitConfig;
    }
}