using UnityEngine;

namespace Code.Units
{
    public interface IUnit
    {
        bool IsDead { get; }
        Transform Transform { get; }
        void Tick();
    }
}