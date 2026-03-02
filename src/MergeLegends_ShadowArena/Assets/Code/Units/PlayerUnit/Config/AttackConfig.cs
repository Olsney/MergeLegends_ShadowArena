using System;
using Code.StaticData;
using UnityEngine;

namespace Code.Units.PlayerUnit.Config
{
    [Serializable]
    public class AttackConfig
    {
        [Range(1,100)]
        public float Damage;
    
        public LayerMaskTypeId TargetMask;
    }
}