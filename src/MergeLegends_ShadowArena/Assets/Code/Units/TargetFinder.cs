using Code.Data;
using UnityEngine;

namespace Code.Units
{
    public class TargetFinder : MonoBehaviour
    {
        [SerializeField] private float _finderRadius;
        [SerializeField] private int _maxColliders;

        private Enemy _currentEnemy;
        private Collider[] _results;
        private float _sqrSearchRadius;

        private void Awake()
        {
            _results = new Collider[_maxColliders];
            _sqrSearchRadius = _finderRadius * _finderRadius;
        }

        public Enemy GetTarget()
            => Find();

        private Enemy Find()
        {
            if (_currentEnemy != null && IsValid() == false)
                _currentEnemy = null;

            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _finderRadius, _results);
            float minSqrDistance = float.MaxValue;

            for (int i = 0; i < hitCount; i++)
            {
                if (_results[i].TryGetComponent<Enemy>(out Enemy enemy) == false)
                    continue;

                if (enemy.IsDeath)
                    continue;

                float sqrDistance = transform.position.SqrMagnitudeTo(enemy.transform.position);

                if (sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    _currentEnemy = enemy;
                }
            }

            return _currentEnemy;
        }

        private bool IsValid()
        {
            if (_currentEnemy == null)
                return false;

            return transform.position.SqrMagnitudeTo(_currentEnemy.transform.position) <= _sqrSearchRadius;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _finderRadius);
        }
    }
}