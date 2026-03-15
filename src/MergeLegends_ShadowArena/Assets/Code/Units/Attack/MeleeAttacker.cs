using Code.StaticData;
using UnityEngine;

namespace Code.Units.Attack
{
    public class MeleeAttacker : MonoBehaviour, IAttacker
    {
        private const float HitRadius = 0.35f;

        public bool IsAttacking { get; private set; }

        private UnitAnimator _animator;
        private AnimationEventsReader _animationEventsReader;
        private UnitNavmeshAgentMover _navmeshAgentMover;

        private Collider[] _hits = new Collider[1];
        private int _layerMask;
        private float _damage;
        private float _attackCooldown;
        private float _currentAttackCooldown;

        private void Awake()
        {
            _animationEventsReader = GetComponentInParent<AnimationEventsReader>();
            _animator = GetComponentInParent<UnitAnimator>();
            _navmeshAgentMover = GetComponentInParent<UnitNavmeshAgentMover>();
        }

        private void OnEnable()
        {
            _animationEventsReader.Attacked += OnAttack;
            _animationEventsReader.AttackEnded += OnAttackEnded;
            _navmeshAgentMover.TargetReached += StartAttack;
        }

        private void OnDisable()
        {
            _animationEventsReader.Attacked -= OnAttack;
            _animationEventsReader.AttackEnded -= OnAttackEnded;
            _navmeshAgentMover.TargetReached -= StartAttack;
        }

        public void Init(float damage, float cooldown, LayerMaskTypeId targetLayerMask)
        {
            _damage = damage;
            _attackCooldown = cooldown;
            _layerMask = 1 << (int)targetLayerMask;
        }

        public void Tick()
        {
            _navmeshAgentMover.RotateToTarget();
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

        private void StartAttack()
        {
            if (CanAttack() == false)
                return;

            _animator.PlayAttack();
            IsAttacking = true;
        }

        private bool CanHit(out Collider hit)
        {
            int hitAmount = Physics.OverlapSphereNonAlloc(transform.position, HitRadius, _hits, _layerMask);

            hit = hitAmount > 0 ? _hits[0] : null;

            return hitAmount > 0;
        }

        private void UpdateCooldown()
        {
            if (_currentAttackCooldown > 0f)
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
}