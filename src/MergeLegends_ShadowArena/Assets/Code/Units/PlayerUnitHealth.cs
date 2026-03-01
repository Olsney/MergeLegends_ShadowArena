using System;
using UnityEngine;

public class PlayerUnitHealth : MonoBehaviour, IHealth
{
    private float _current;
    private float _max;

    public event Action HealthChanged;

    public float Current => _current;

    public float Max => _max;

    public void TakeDamage(float damage)
    {
        _current -= damage;

        HealthChanged?.Invoke();
    }
}