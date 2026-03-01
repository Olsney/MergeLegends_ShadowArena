using System;
using UnityEngine;

namespace Code.Units.PlayerUnit.Config
{
    [Serializable]
    public class PlayerUnitConfig
    {
        [Range(1,100)]
        public int Hp;
    
        [Range(1,30)]
        public float Damage;

        [Range(0,10)]
        public float MoveSpeed;

        public GameObject PlayerUnitPrefab;
    }
}