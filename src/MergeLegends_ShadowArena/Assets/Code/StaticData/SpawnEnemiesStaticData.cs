using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "SpawnerData", menuName = "StaticData/EnemiesSpawner")]
    public class SpawnEnemiesStaticData : ScriptableObject
    {
        public GameLevelTypeId GameLevelTypeId;
        public List<EnemySpawnData> EnemiesSpawnData;
    }
}