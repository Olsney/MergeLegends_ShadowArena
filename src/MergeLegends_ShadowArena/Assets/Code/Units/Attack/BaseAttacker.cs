using UnityEngine;

namespace Code.Units.Attack
{
    public abstract class BaseAttacker : MonoBehaviour, IAttacker
    {
        public bool IsAttacking { get; protected set; }

        protected UnitAnimator _animator;
        protected AnimationEventsReader _animationEventsReader;
        protected UnitNavmeshAgentMover NavmeshAgentMover;

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
            NavmeshAgentMover = GetComponentInParent<UnitNavmeshAgentMover>();
        }

        protected virtual void OnStart()
        {
            _animationEventsReader.Attacked += OnAttack;
            _animationEventsReader.AttackEnded += OnAttackEnded;
            NavmeshAgentMover.TargetReached += StartAttack;
        }

        protected virtual void OnDestroyInternal()
        {
            _animationEventsReader.Attacked -= OnAttack;
            _animationEventsReader.AttackEnded -= OnAttackEnded;
            NavmeshAgentMover.TargetReached -= StartAttack;
        }

        protected virtual void OnAttackEnded()
        {
            _currentAttackCooldown = _attackCooldown;
            IsAttacking = false;
        }

        protected virtual void StartAttack()
        {
            if (CanAttack() == false)
                return;
            
            _animator.PlayAttack();
            IsAttacking = true;
        }

        protected abstract void OnAttack();

        public virtual void Tick()
        {
            UpdateCooldown();
            NavmeshAgentMover.RotateToTarget();
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