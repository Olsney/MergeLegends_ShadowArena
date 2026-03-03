using System;
using UnityEngine;

namespace Code.Units.PlayerUnit.Config
{
    [Serializable]
    public class UnitConfig
    {
        [Range(0,10)]
        public float MoveSpeed;

        [Range(0,100)]
        public float AttackDistance;

        [Range(0,5)]
        public float AttackCooldown;
    }
}