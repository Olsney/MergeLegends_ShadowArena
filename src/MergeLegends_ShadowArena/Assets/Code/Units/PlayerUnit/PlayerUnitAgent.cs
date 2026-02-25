using Code.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.PlayerUnit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerUnitAgent : MonoBehaviour
    {
        private const float SqrDistance = 1.0f;

        private NavMeshAgent _navMeshAgent;
        private TargetFinder _targetFinder;
        private Enemy _enemy;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _targetFinder = GetComponent<TargetFinder>();
        }

        public void Move()
        {
            if (_enemy == null)
                _enemy = _targetFinder.GetTarget();

            if (_enemy == null)
                return;

            if (_enemy.IsDeath)
                return;

            if (IsNeedToMove())
                _navMeshAgent.destination = _enemy.transform.position;
        }

        private bool IsNeedToMove()
        {
            if (_navMeshAgent == null)
                return false;

            return transform.position.SqrMagnitudeTo(_targetFinder.GetTarget().transform.position) >= SqrDistance;
        }
    }
}