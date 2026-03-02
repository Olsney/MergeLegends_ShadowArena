using System;
using Code.Data;
using Code.Units.PlayerUnit.Config;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.PlayerUnit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitAgentMover : MonoBehaviour
    {
        private const float SqrDistance = 1.5f;

        private NavMeshAgent _navMeshAgent;
        private ITargetFinder _targetFinder;
        private MeleeAttacker meleeAttacker;
        private AnimateAlongAgent _animateAlongAgent;
        private Enemy _enemyTarget;

        public event Action<Transform> TargetReached;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targetFinder = GetComponent<ITargetFinder>();
            meleeAttacker = GetComponentInChildren<MeleeAttacker>();
            _animateAlongAgent = GetComponentInChildren<AnimateAlongAgent>();
        }

        public void InitConfig(PlayerUnitConfig playerUnitConfig)
        {
            _navMeshAgent.speed = playerUnitConfig.MoveSpeed;
        }

        public void Move()
        {
            UpdateTarget();
            HandleMovement();
            UpdateAnimation();
        }

        private void UpdateTarget()
        {
            var newTarget = _targetFinder.GetTarget();

            if (newTarget != _enemyTarget)
                _enemyTarget = newTarget;
        }

        private void HandleMovement()
        {
            if (_enemyTarget == null || _enemyTarget.IsDead)
            {
                StopMoving();
                return;
            }

            if (IsNeedToMove() && meleeAttacker.IsAttacking == false)
            {
                _navMeshAgent.destination = _enemyTarget.transform.position;
            }
            else
            {
                StopMoving();
                TargetReached?.Invoke(_enemyTarget.transform);
            }
        }

        private void UpdateAnimation()
            => _animateAlongAgent.MoveAnimation();

        private void StopMoving()
        {
            if (_navMeshAgent.hasPath)
                _navMeshAgent.ResetPath();
        }

        private bool IsNeedToMove()
        {
            if (_navMeshAgent == null || _enemyTarget == null)
                return false;

            return transform.position.SqrMagnitudeTo(_enemyTarget.transform.position) >= SqrDistance;
        }
    }
}