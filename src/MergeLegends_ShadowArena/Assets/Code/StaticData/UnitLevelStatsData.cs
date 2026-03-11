using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class UnitLevelStatsData
    {
        [Range(0, 1000)]
        public float Hp;

        [Range(0, 500)]
        public float Damage;

        public GameObject ViewPrefab;
    }
}