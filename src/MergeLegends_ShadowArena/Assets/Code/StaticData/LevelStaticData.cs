using System.Collections.Generic;
using Code.Logic.PlayArea.GridConfig;
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
        public List<EnemyTypeSpawnConfig> EnemiesSpawnData;
        
        [HideInInspector]
        public IReadOnlyList<EnemyTypeSpawnConfig> GetTypeEneimies()
        {
            return EnemiesSpawnData;
        }
    }
}