using Code.Data;
using UnityEngine;

namespace Code.Units
{
    public class TargetFinder : MonoBehaviour, ITargetFinder
    {
        private const int MaxColliders = 20;

        private float _finderRadius;
        private int _layerMask;
        private Collider[] _results = new Collider[MaxColliders];
        private Enemy _currentEnemy;

        public void InitConfig(TargetFinderConfig config)
        {
            _finderRadius = config.Radius;
            _layerMask = 1 << (int)config.TargetMask;
        }

        public Enemy GetTarget()
        {
            if (_currentEnemy == null || _currentEnemy.IsDead)
                return Find();

            return _currentEnemy;
        }

        private Enemy Find()
        {
            _currentEnemy = null;

            int hitCount = Physics.OverlapSphereNonAlloc(
                transform.position,
                _finderRadius,
                _results,
                _layerMask
            );

            float minSqrDistance = float.MaxValue;

            for (int i = 0; i < hitCount; i++)
            {
                var enemy = _results[i].GetComponent<Enemy>();

                if (enemy == null || enemy.IsDead)
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _finderRadius);
        }
    }
}