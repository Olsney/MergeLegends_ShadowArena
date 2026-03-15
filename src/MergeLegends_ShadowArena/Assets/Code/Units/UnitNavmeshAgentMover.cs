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

        private NavMeshAgent _navMeshAgent;
        private ITargetFinder _targetFinder;
        private IAttacker _attacker;
        private AnimateAlongNavmeshAgent _animateAlongNavmeshAgent;
        private IUnit _unitTarget;

        public event Action TargetReached;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targetFinder = GetComponent<ITargetFinder>();
            _attacker = GetComponentInChildren<IAttacker>();
            _animateAlongNavmeshAgent = GetComponentInChildren<AnimateAlongNavmeshAgent>();
        }

        public void InitConfig(float speed, float attackDistance)
        {
            _navMeshAgent.speed = speed;
            _sqrStopDistance = attackDistance * attackDistance;
        }

        public void Tick()
        {
            UpdateTarget();
            HandleMovement();
            UpdateAnimation();
        }

        public void RotateToTarget()
        {
            if (_unitTarget == null)
                return;

            transform.LookAt(_unitTarget.Transform);
        }

        public Transform GetCurrentTargetTransform()
        {
            if (_unitTarget == null)
                return null;
            
            return _unitTarget.Transform;
        }

        private void UpdateTarget()
        {
            var newTarget = _targetFinder.GetTarget();

            if (newTarget != _unitTarget)
                _unitTarget = newTarget;
        }

        private void HandleMovement()
        {
            if (_unitTarget == null || _unitTarget.IsDead)
            {
                StopMoving();
                return;
            }

            if (IsNeedToMove() && _attacker.IsAttacking == false)
            {
                _navMeshAgent.destination = _unitTarget.Transform.position;
            }
            else
            {
                StopMoving();
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

        private bool IsNeedToMove()
        {
            if (_navMeshAgent == null || _unitTarget == null)
                return false;

            return transform.position.SqrMagnitudeTo(_unitTarget.Transform.position) >= _sqrStopDistance;
        }
    }
}