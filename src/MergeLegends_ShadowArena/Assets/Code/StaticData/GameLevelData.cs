using System;
using System.Collections.Generic;

namespace Code.StaticData
{
    [Serializable]
    public class GameLevelData
    {
        public List<EnemySpawnData> EnemiesSpawnData;
        public int VictoryRewardValue;
        public int LoseRewardValue;
    }
}