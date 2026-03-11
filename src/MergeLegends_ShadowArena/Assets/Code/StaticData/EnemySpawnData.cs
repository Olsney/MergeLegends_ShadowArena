using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class EnemySpawnData
    {
        [Min(0)] 
        public int LevelIndex;

        public UnitAttackTypeId UnitAttackTypeId;
    }
}