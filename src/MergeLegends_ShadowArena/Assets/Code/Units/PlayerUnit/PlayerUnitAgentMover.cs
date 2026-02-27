using Code.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.PlayerUnit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnitAgentMover : MonoBehaviour
    {
        private const float SqrDistance = 1.0f;

        private NavMeshAgent _navMeshAgent;
        private TargetFinder _targetFinder;
        private Enemy _enemyTarget;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targetFinder = GetComponent<TargetFinder>();
        }

        public void Move()
        {
            UpdateTarget();
            
            if (_enemyTarget == null || _enemyTarget.IsDead)
            {
                StopMoving();
                return;
            }

            if (IsNeedToMove())
                _navMeshAgent.destination = _enemyTarget.transform.position;
        }
        
        private void UpdateTarget()
        {
            var newTarget = _targetFinder.GetTarget();
            
            if (newTarget != _enemyTarget)
                _enemyTarget = newTarget;
        }
        
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