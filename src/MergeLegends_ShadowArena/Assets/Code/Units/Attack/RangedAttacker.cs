using Code.Infrastructure.Factory.Game;
using Code.StaticData;
using Code.Units.MagicSphereAttack;
using UnityEngine;
using Zenject;

namespace Code.Units.Attack
{
    public class RangedAttacker : MonoBehaviour, IAttacker
    {
        public bool IsAttacking { get; private set; }

        private IGameFactory _gameFactory;
        private UnitAnimator _animator;
        private AnimationEventsReader _animationEventsReader;
        private UnitNavmeshAgentMover _navmeshAgentMover;
        private MagicSphere _magicSphere;

        private float _attackCooldown;
        private float _currentAttackCooldown;

        [Inject]
        private void Construct(IGameFactory gameFactory)
            => _gameFactory = gameFactory;

        private void Awake()
        {
            _animationEventsReader = GetComponentInParent<AnimationEventsReader>();
            _animator = GetComponentInParent<UnitAnimator>();
            _navmeshAgentMover = GetComponentInParent<UnitNavmeshAgentMover>();
            _magicSphere = _gameFactory.CreateMagicSpherePlayerUnit(transform.position).GetComponent<MagicSphere>();
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

        public void Init(float damage, float cooldown, LayerMaskTypeId targetLayerMask, float magicSphereSpeed)
        {
            _attackCooldown = cooldown;
            _magicSphere.Init(damage, targetLayerMask, magicSphereSpeed);
        }

        public void Tick()
        {
            UpdateCooldown();
            _magicSphere.Tick();
        }

        private void OnAttack()
        {
            if (_navmeshAgentMover.GetCurrentTargetTransform() == null)
                return;

            _magicSphere.Launch(transform.position, _navmeshAgentMover.GetCurrentTargetTransform());
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