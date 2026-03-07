using System;
using UnityEngine;

public class AnimationEventsReader : MonoBehaviour
{
    public event Action Attacked;
    public event Action AttackEnded;
    
    private void OnAttack() 
        => Attacked?.Invoke();

    private void OnAttackEnded() 
        => AttackEnded?.Invoke();
}
