using System;
using Code.Units.PlayerUnit.Config;

public interface IHealth
{
    event Action HealthChanged;
    float CurrentValue { get; }
    float MaxValue { get; }
    void TakeDamage(float damage);
    void InitConfig(UnitConfig unitConfig);
}