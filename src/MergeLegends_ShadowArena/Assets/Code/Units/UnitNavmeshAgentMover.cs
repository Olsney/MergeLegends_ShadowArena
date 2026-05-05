using System;
using Code.Data;
using Code.Units.Animation;
using Code.Units.Attack;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitNavmeshAgentMover : MonoBehaviour
    {
        private float _sqrStopDistance;

        private UnitDeath _unitDeath;
        private NavMeshAgent _navMeshAgent;
        private ITargetFinder _targetFinder;
        private IAttacker _attacker;
        private AnimateAlongNavmeshAgent _animateAlongNavmeshAgent;
        private IUnit _unitTarget;
        private bool _isActive;
        private bool _isTargetReached;

        public event Action TargetReached;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targetFinder = GetComponent<ITargetFinder>();
            _attacker = GetComponentInChildren<IAttacker>();
            _animateAlongNavmeshAgent = GetComponentInChildren<AnimateAlongNavmeshAgent>();
            _unitDeath = GetComponent<UnitDeath>();
        }

        private void Start()
            => _unitDeath.Dead += DisableAgent;

        private void OnDestroy()
            => _unitDeath.Dead -= DisableAgent;

        public void InitConfig(float speed, float attackDistance)
        {
            _navMeshAgent.speed = speed;
            _sqrStopDistance = attackDistance;
        }

        public void StartMoving()
            => _isActive = true;

        private void Update()
        {
            if (_isActive == false)
                return;

            UpdateTarget();

            if (_isTargetReached)
                RotateToTarget();

            HandleMovement();
            UpdateAnimation();
        }

        private void RotateToTarget()
        {
            if (_unitTarget == null)
                return;

            transform.LookAt(_unitTarget.Transform);
        }

        public IUnit GetCurrentTarget()
        {
            if (_unitTarget == null)
                return null;

            return _unitTarget;
        }

        private void UpdateTarget()
        {
            var newTarget = _targetFinder.GetTarget();

            if (newTarget != _unitTarget)
            {
                _unitTarget = newTarget;
                _isTargetReached = false;
            }
        }

        private void HandleMovement()
        {
            if (_unitTarget == null || _unitTarget.IsDead)
            {
                StopMoving();
                return;
            }

            if (_navMeshAgent.enabled == false || _navMeshAgent.isOnNavMesh == false)
                return;

            if (IsNeedToMove() && _attacker.IsAttacking == false)
            {
                _isTargetReached = false;
                _navMeshAgent.destination = _unitTarget.Transform.position;
            }
            else
            {
                StopMoving();
                _isTargetReached = true;
                TargetReached?.Invoke();
            }
        }

        private void UpdateAnimation()
            => _animateAlongNavmeshAgent.MoveAnimation();

        private void StopMoving()
        {
            if (_navMeshAgent.hasPath)
                _navMeshAgent.ResetPath();
        }

        private void DisableAgent()
        {
            StopMoving();
            _navMeshAgent.enabled = false;
        }

        private bool IsNeedToMove()
        {
            if (_navMeshAgent == null || _unitTarget == null)
                return false;

            return transform.position.SqrMagnitudeTo(_unitTarget.Transform.position) >= _sqrStopDistance;
        }
    }
}