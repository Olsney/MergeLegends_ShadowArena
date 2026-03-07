using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class UnitData
    {
        [Range(0,10)]
        public float MoveSpeed;

        [Range(0,100)]
        public float AttackDistance;

        [Range(0,5)]
        public float AttackCooldown;
    }
}