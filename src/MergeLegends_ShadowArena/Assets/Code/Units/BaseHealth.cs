using System;
using UnityEngine;

namespace Code.Units
{
    public class BaseHealth : MonoBehaviour, IHealth
    {
        private float _currentValue;

        public float MaxValue { get; private set; }
    
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

        public void InitConfig(float hp)
        {
            _currentValue = hp;
            MaxValue = hp;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentValue <= 0)
                return;
            
            CurrentValue = Mathf.Clamp(CurrentValue - damage, 0, MaxValue);
        }
    }
}