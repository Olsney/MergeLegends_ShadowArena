using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class UnitLevelStatsData
    {
        [Min(0)]
        public float Hp;

        [Min(0)]
        public float Damage;

        public GameObject ViewPrefab;
    }
}