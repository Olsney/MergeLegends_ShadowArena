using System;
using Code.Units.Effects;
using UnityEngine;

namespace Code.Units
{
    public interface IUnit
    {
        event Action Destroying;
        public UnitDeath UnitDeath { get; }
        public UnitEffects Effects { get; }
        bool IsDead { get; }
        Transform Transform { get; }
        void ProcessUnit();
        void StartVictoryAnimation();
    }
}