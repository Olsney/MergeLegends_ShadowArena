using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class UnitLvlStatsConfig
    {
        [Range(0, 1000)]
        public float Hp;

        [Range(0, 500)]
        public float Damage;

        public GameObject ViewPrefab;
    }
}