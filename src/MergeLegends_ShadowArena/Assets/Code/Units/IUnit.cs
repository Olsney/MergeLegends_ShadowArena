using System;
using UnityEngine;

namespace Code.Units
{
    public interface IUnit
    {
        event Action Destroying;
        bool IsDead { get; }
        Transform Transform { get; }
        void ProcessUnit();
    }
}