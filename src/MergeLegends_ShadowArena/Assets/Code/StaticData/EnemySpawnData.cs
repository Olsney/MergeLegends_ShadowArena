using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class EnemySpawnData
    {
        [Range(0, 100)] 
        public int LevelIndex;

        public UnitAttackTypeId UnitAttackTypeId;
    }
}