using Code.Infrastructure.Factory.Game;
using Code.Units.Config;
using Code.Units.PlayerUnit;
using Code.Units.PlayerUnit.Config;
using UnityEngine;
using Zenject;

namespace Code.Units
{
    public class RangedAttacker : MonoBehaviour, IAttacker
    {
        public bool IsAttacking { get; private set; }

        private IGameFactory _gameFactory;
        private UnitAnimator _animator;
        private AnimationEventsReader _animationEventsReader;
        private UnitAgentMover _agentMover;

        private float _attackCooldown;
        private float _currentAttackCooldown;
        private Transform _targetTransform;
        private MagicSphere _magicSphere;
        private bool _attackIsActive;

        [Inject]
        private void Construct(IGameFactory gameFactory) 
            => _gameFactory = gameFactory;

        private void Awake()
        {
            _animationEventsReader = GetComponentInParent<AnimationEventsReader>();
            _animator = GetComponentInParent<UnitAnimator>();
            _agentMover = GetComponentInParent<UnitAgentMover>();
            _magicSphere = _gameFactory.CreateMagicSpherePlayerUnit(transform.position).GetComponent<MagicSphere>();
        }

        private void Start()
        {
            _animationEventsReader.Attacked += OnAttack;
            _animationEventsReader.AttackEnded += OnAttackEnded;
            _agentMover.TargetReached += EnableAttack;
            _agentMover.TargetLosted += DisableAttack;
        }

        private void OnDestroy()
        {
            _animationEventsReader.Attacked -= OnAttack;
            _animationEventsReader.AttackEnded -= OnAttackEnded;
            _agentMover.TargetReached -= EnableAttack;
            _agentMover.TargetLosted -= DisableAttack;
        }

        public void InitConfig(TargetConfig targetConfig, UnitConfig unitConfig)
        {
            _attackCooldown = unitConfig.AttackCooldown;
            _magicSphere.InitConfig(targetConfig, unitConfig);
        }

        public void Tick()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();

            _magicSphere.Tick();
        }

        private void EnableAttack(Transform target)
        {
            _targetTransform = target;
            _attackIsActive = true;
        }

        private void DisableAttack()
        {
            _targetTransform = null;
            _attackIsActive = false;
        }

        private void OnAttack() 
            => _magicSphere.Launch(transform.position, transform.forward);

        private void OnAttackEnded()
        {
            _currentAttackCooldown = _attackCooldown;
            IsAttacking = false;
        }

        private void StartAttack()
        {
            _agentMover.transform.LookAt(_targetTransform);
            _animator.PlayAttack();
            IsAttacking = true;
        }

        private void UpdateCooldown()
        {
            if (_currentAttackCooldown <= 0f == false)
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            _attackIsActive && IsAttacking == false && _currentAttackCooldown <= 0f;
    }
}