using System;
using UnityEngine;

namespace Code.Units
{
    public interface IUnit
    {
        event Action ProcessStarting;
        event Action Destroying;
        public UnitDeath UnitDeath { get; }
        bool IsDead { get; }
        Transform Transform { get; }
        void ProcessUnit();
        void StartVictoryAnimation();
    }
}