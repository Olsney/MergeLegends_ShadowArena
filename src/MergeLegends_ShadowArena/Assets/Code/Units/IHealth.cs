using System;

namespace Code.Units
{
    public interface IHealth
    {
        event Action HealthChanged;
        float CurrentValue { get; }
        float MaxValue { get; }
        void TakeDamage(float damage);
        void Init(float maxHp);
    }
}