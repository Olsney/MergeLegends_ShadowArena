using System.Linq;
using Code.Units;
using Code.Units.PlayerUnit;
using Code.Units.PlayerUnit.Config;
using UnityEngine;

public class MeleeAttacker : MonoBehaviour
{
    private const float Cleavage = 0.22f;

    public bool IsAttacking { get; private set; }

    private UnitAnimator _animator;
    private AnimationEventsReader _animationEventsReader;
    private UnitAgentMover _agentMover;

    private Collider[] _hits = new Collider[1];
    private int _layerMask;
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

    public void InitConfig(AttackConfig attackConfig)
    {
        _damage = attackConfig.Damage;
        _layerMask = 1 << (int)attackConfig.TargetMask;
    }

    private void OnAttack()
    {
        if (Hit(out Collider hit))
        {
            hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
        }
    }

    private void OnAttackEnded()
    {
        IsAttacking = false;
    }

    private bool Hit(out Collider hit)
    {
        var hitAmount = Physics.OverlapSphereNonAlloc(transform.position, Cleavage, _hits, _layerMask);
        
        hit = _hits.FirstOrDefault();

        return hitAmount > 0;
    }

    private void StartAttack(Transform target)
    {
        if (IsAttacking)
            return;

        _agentMover.transform.LookAt(target);
        _animator.PlayAttack();
        IsAttacking = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Cleavage);
    }
}