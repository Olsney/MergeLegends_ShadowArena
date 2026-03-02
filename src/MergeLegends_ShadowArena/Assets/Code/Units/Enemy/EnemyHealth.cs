using System;
using Code.Units;
using Code.Units.PlayerUnit.Config;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    private float _currentValue = 100;

    public float MaxValue { get; private set; } = 100;
    
    public float CurrentValue 
    { 
        get => _currentValue;
        private set
        {
            if (value != _currentValue)
            {
                _currentValue = value;
          
                HealthChanged?.Invoke();
            }
        }
    }

    public event Action HealthChanged;

    public void InitConfig(UnitConfig unitConfig)
    {
        _currentValue = unitConfig.Hp;
        MaxValue = unitConfig.Hp;
    }

    public void TakeDamage(float damage)
    {
        if (CurrentValue <= 0)
            return;
        
        CurrentValue = Mathf.Clamp(CurrentValue - damage, 0, MaxValue);
    }
}