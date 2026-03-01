using System;
using System.Collections;
using Code.Units;
using UnityEngine;


public class PlayerUnitDeath : MonoBehaviour
{
    public PlayerUnitHealth _health;
    private UnitAnimator _animator;

    public event Action Happened;

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
        if (_health.Current <= 0)
            Die();
    }

    private void Die()
    {
        _health.HealthChanged -= OnHealthChanged;

        _animator.PlayDeath();

        StartCoroutine(DestroyTimer());

        Happened?.Invoke();
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}