using Code.Infrastructure.Factory.Projectile;
using Code.StaticData;
using Code.Units.Audio;
using UnityEngine;
using Zenject;

namespace Code.Units.Attack
{
    public class RangedAttacker : MonoBehaviour, IAttacker
    {
        public bool IsAttacking { get; private set; }

        private UnitTypeId _unitTypeId;

        private IProjectileFactory _projectileFactory;
        private UnitAnimator _animator;
        private AnimationEventsReader _animationEventsReader;
        private UnitNavmeshAgentMover _navmeshAgentMover;
        private UnitDeath _unitDeath;
        private RangedAttackSound _rangedAttackSound;

        private float _attackCooldown;
        private float _currentAttackCooldown;
        private IUnit _targetToAttack;

        private float _damage;
        private LayerMaskTypeId _targetLayerMask;
        private float _magicSphereSpeed;
        private bool _isActive;

        [Inject]
        private void Construct(IProjectileFactory projectileFactory)
            => _projectileFactory = projectileFactory;

        private void Awake()
        {
            _animationEventsReader = GetComponentInParent<AnimationEventsReader>();
            _animator = GetComponentInParent<UnitAnimator>();
            _navmeshAgentMover = GetComponentInParent<UnitNavmeshAgentMover>();
            _unitDeath = GetComponentInParent<UnitDeath>();
        }

        private void OnEnable()
        {
            _animationEventsReader.Attacked += OnAttack;
            _animationEventsReader.AttackEnded += OnAttackEnded;
            _navmeshAgentMover.TargetReached += StartAttack;
            _unitDeath.Dead += StopAttacking;
        }

        private void OnDisable()
        {
            _animationEventsReader.Attacked -= OnAttack;
            _animationEventsReader.AttackEnded -= OnAttackEnded;
            _navmeshAgentMover.TargetReached -= StartAttack;
            _unitDeath.Dead -= StopAttacking;
        }

        private void Update()
        {
            if (_isActive == false)
                return;

            UpdateCooldown();
        }

        public void Init(float damage, float cooldown, LayerMaskTypeId targetLayerMask, float magicSphereSpeed,
            UnitTypeId unitTypeId, RangedAttackSound rangedAttackSound)
        {
            _attackCooldown = cooldown;
            _unitTypeId = unitTypeId;

            _damage = damage;
            _targetLayerMask = targetLayerMask;
            _magicSphereSpeed = magicSphereSpeed;
            _rangedAttackSound = rangedAttackSound;
        }

        public void StartAttacking()
            => _isActive = true;

        private void StopAttacking()
            => _isActive = false;

        private void OnAttack()
        {
            _targetToAttack = _navmeshAgentMover.GetCurrentTarget();

            if (_targetToAttack == null)
                return;

            _projectileFactory.CreateMagicSphere(_unitTypeId, transform.position)
                .Launch(_damage, _targetLayerMask, _magicSphereSpeed, transform.position, _targetToAttack,
                    _rangedAttackSound);
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