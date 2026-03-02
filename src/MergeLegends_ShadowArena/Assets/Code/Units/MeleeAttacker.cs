using System.Linq;
using Code.Units;
using Code.Units.Config;
using Code.Units.PlayerUnit;
using Code.Units.PlayerUnit.Config;
using UnityEngine;

public class MeleeAttacker : MonoBehaviour, IAttacker
{
    private const float HitRadius = 0.35f;

    public bool IsAttacking { get; private set; }

    private UnitAnimator _animator;
    private AnimationEventsReader _animationEventsReader;
    private UnitAgentMover _agentMover;

    private Collider[] _hits = new Collider[1];
    private int _layerMask;
    private float _attackCooldown;
    private float _currentAttackCooldown;
    private Transform _targetTransform;
    private float _damage;

    private void Awake()
    {
        _animationEventsReader = GetComponentInParent<AnimationEventsReader>();
        _animator = GetComponentInParent<UnitAnimator>();
        _agentMover = GetComponentInParent<UnitAgentMover>();
    }

    private void Start()
    {
        _animationEventsReader.Attacked += OnAttack;
        _animationEventsReader.AttackEnded += OnAttackEnded;
        _agentMover.TargetReached += StartAttack;
    }

    private void OnDestroy()
    {
        _animationEventsReader.Attacked -= OnAttack;
        _animationEventsReader.AttackEnded -= OnAttackEnded;
        _agentMover.TargetReached -= StartAttack;
    }

    public void InitConfig(TargetConfig targetConfig, UnitConfig unitConfig)
    {
        _damage = unitConfig.Damage;
        _layerMask = 1 << (int)targetConfig.TargetMask;
        _attackCooldown = unitConfig.AttackCooldown;
    }

    public void Tick()
    {
        UpdateCooldown();
    }

    private void OnAttack()
    {
        if (CanHit(out Collider hit))
        {
            hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
        }
    }

    private void OnAttackEnded()
    {
        _currentAttackCooldown = _attackCooldown;
        IsAttacking = false;
    }

    private bool CanHit(out Collider hit)
    {
        var hitAmount = Physics.OverlapSphereNonAlloc(transform.position, HitRadius, _hits, _layerMask);
        
        hit = _hits.FirstOrDefault();
        
        return hitAmount > 0;
    }

    private void StartAttack(Transform target)
    {
        if (CanAttack() == false)
            return;

        _agentMover.transform.LookAt(target);
        _animator.PlayAttack();
        IsAttacking = true;
    }

    private void UpdateCooldown()
    {
        if (_currentAttackCooldown <= 0f == false)
            _currentAttackCooldown -= Time.deltaTime;
    }

    private bool CanAttack() =>
        IsAttacking == false && _currentAttackCooldown <= 0f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, HitRadius);
    }
    
}