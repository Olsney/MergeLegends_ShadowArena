using System;
using System.Collections;
using UnityEngine;

namespace Code.Units
{
    public class UnitDeath : MonoBehaviour
    {
        private IHealth _health;
        private UnitAnimator _animator;

        public event Action Dead;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _animator = GetComponentInChildren<UnitAnimator>();
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
            _health.HealthChanged -= OnHealthChanged;

            _animator.PlayDeath();

            StartCoroutine(DestroyTimer());

            Dead?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}