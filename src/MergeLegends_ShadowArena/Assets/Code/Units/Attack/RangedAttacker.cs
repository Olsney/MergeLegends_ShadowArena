using Code.StaticData;
using Code.Units.MagicSphereAttack;
using UnityEngine;

namespace Code.Units.Attack
{
    public class RangedAttacker : MonoBehaviour, IAttacker
    {
        public bool IsAttacking { get; private set; }

        private UnitAnimator _animator;
        private AnimationEventsReader _animationEventsReader;
        private UnitNavmeshAgentMover _navmeshAgentMover;
        private MagicSphere _magicSphere;
        private IUnit _targetUnit;

        private bool _isTargetUnitDead;
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

        public void Init(float damage, float cooldown, LayerMaskTypeId targetLayerMask, float magicSphereSpeed,
            UnitDeath unitDeath, MagicSphere magicSphere)
        {
            _attackCooldown = cooldown;
            _magicSphere = magicSphere;
            _magicSphere.Init(damage, targetLayerMask, magicSphereSpeed, unitDeath);
        }

        public void Tick()
        {
            _navmeshAgentMover.RotateToTarget();
            _magicSphere.Tick();
            UpdateCooldown();

            if (_targetUnit == null || _targetUnit.IsDead)
            {
                if (_isTargetUnitDead == false)
                {
                    _isTargetUnitDead = true;
                    _magicSphere.ResetTargetTransform();
                }
            }
        }

        private void OnAttack()
        {
            _targetUnit = _navmeshAgentMover.GetCurrentTarget();

            if (_navmeshAgentMover.GetCurrentTarget() == null || _magicSphere == null)
                return;

            _isTargetUnitDead = false;
            _magicSphere.Launch(transform.position, _navmeshAgentMover.GetCurrentTarget().Transform);
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

        private void UpdateCooldown()
        {
            if (_currentAttackCooldown > 0f)
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            IsAttacking == false && _currentAttackCooldown <= 0f;
    }
}