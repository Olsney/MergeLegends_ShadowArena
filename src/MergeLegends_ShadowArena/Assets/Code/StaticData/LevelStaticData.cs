using Code.Logic.PlayArea.GridSettings;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public Vector3 PlayZonePosition;
        public Vector3 HeroesGridPlacesPosition;
        public Vector3 EnemiesGridPlacesPosition;
        public GridConfig GridConfig;
    }
}