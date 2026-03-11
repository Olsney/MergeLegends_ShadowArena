using System;
using UnityEngine;

namespace Code.Units
{
    [RequireComponent(typeof(BoxCollider))]
    public class UnitDeath : MonoBehaviour
    {
        private const float TimeToDestroy = 3f;

        private IHealth _health;
        private UnitAnimator _animator;
        private BoxCollider _collider;

        public event Action Dead;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _animator = GetComponentInChildren<UnitAnimator>();
            _collider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            if (_health.CurrentValue <= 0)
                Die();
        }

        private void Die()
        {
            _collider.enabled = false;
            _health.HealthChanged -= OnHealthChanged;
            _animator.PlayDeath();
            Dead?.Invoke();
            Destroy(gameObject, TimeToDestroy);
        }
    }
}