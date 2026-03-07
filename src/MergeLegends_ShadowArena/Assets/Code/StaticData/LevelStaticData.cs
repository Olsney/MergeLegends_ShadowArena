using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public Vector3 PlayZonePosition;
        public Vector3 PlayerUnitsGridPlacesPosition;
        public Vector3 EnemiesGridPlacesPosition;
    }
}