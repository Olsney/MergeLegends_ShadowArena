using System;

public interface IHealth
{
    event Action HealthChanged;
    float CurrentValue { get; }
    float MaxValue { get; }
    void TakeDamage(float damage);
    void InitConfig(float maxHp);
}