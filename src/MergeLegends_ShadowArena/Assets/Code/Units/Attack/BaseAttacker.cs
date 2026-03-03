using Code.Units;
using Code.Units.Attack;
using Code.Units.Config;
using Code.Units.PlayerUnit.Config;
using UnityEngine;

public abstract class BaseAttacker : MonoBehaviour, IAttacker
{
    public bool IsAttacking { get; protected set; }

    protected UnitAnimator _animator;
    protected AnimationEventsReader _animationEventsReader;
    protected UnitAgentMover _agentMover;

    protected float _attackCooldown;
    protected float _currentAttackCooldown;

    private void Awake()
        => OnAwake();

    private void Start()
        => OnStart();

    private void OnDestroy()
        => OnDestroyInternal();

    protected virtual void OnAwake()
    {
        _animationEventsReader = GetComponentInParent<AnimationEventsReader>();
        _animator = GetComponentInParent<UnitAnimator>();
        _agentMover = GetComponentInParent<UnitAgentMover>();
    }

    protected virtual void OnStart()
    {
        _animationEventsReader.Attacked += OnAttack;
        _animationEventsReader.AttackEnded += OnAttackEnded;
        _agentMover.TargetReached += StartAttack;
    }

    protected virtual void OnDestroyInternal()
    {
        _animationEventsReader.Attacked -= OnAttack;
        _animationEventsReader.AttackEnded -= OnAttackEnded;
        _agentMover.TargetReached -= StartAttack;
    }

    protected virtual void OnAttackEnded()
    {
        _currentAttackCooldown = _attackCooldown;
        IsAttacking = false;
    }

    protected virtual void StartAttack(Transform target)
    {
        if (CanAttack() == false)
            return;

        _agentMover.transform.LookAt(target);
        _animator.PlayAttack();
        IsAttacking = true;
    }

    protected abstract void OnAttack();

    public abstract void InitConfig(TargetConfig targetConfig, UnitConfig unitConfig);

    public virtual void Tick()
    {
        UpdateCooldown();
    }

    private void UpdateCooldown()
    {
        if (_currentAttackCooldown > 0f)
            _currentAttackCooldown -= Time.deltaTime;
    }

    private bool CanAttack() =>
        IsAttacking == false && _currentAttackCooldown <= 0f;
}