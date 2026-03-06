using UnityEngine;
using UnityEngine.AI;

namespace Code.Units.Animation
{
    public class AnimateAlongNavmeshAgent : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;

        private NavMeshAgent _agent;
        private UnitAnimator _animator;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<UnitAnimator>();
        }

        public void MoveAnimation()
        {
            if (ShouldMove())
                _animator.Move(_agent.velocity.magnitude);
            else
                _animator.StopMoving();
        }

        private bool ShouldMove() =>
            _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
    }
}