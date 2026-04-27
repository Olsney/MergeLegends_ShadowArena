using System;
using UnityEngine;

namespace Code.Units.Enemies
{
    public class EnemyHealth : MonoBehaviour, IHealth
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

        public void Init(float hp)
        {
            _currentValue = hp;
            MaxValue = hp;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentValue <= 0 || damage <= 0)
                return;

            CurrentValue = Mathf.Clamp(CurrentValue - damage, 0, MaxValue);
        }
    }
}