using Code.Data;
using Code.StaticData;
using UnityEngine;

namespace Code.Units
{
    public class TargetFinder : MonoBehaviour, ITargetFinder
    {
        private const int MaxColliders = 20;
        private const float FinderRadius = 22;

        private int _layerMask;
        private Collider[] _results = new Collider[MaxColliders];
        private IUnit _currentUnit;

        public void Init(LayerMaskTypeId layerMaskTypeId)
        {
            _layerMask = 1 << (int)layerMaskTypeId;
        }

        public IUnit GetTarget()
        {
            if (_currentUnit == null || _currentUnit.IsDead)
                return Find();

            return _currentUnit;
        }

        private IUnit Find()
        {
            _currentUnit = null;

            int hitCount = Physics.OverlapSphereNonAlloc(
                transform.position,
                FinderRadius,
                _results,
                _layerMask
            );

            float minSqrDistance = float.MaxValue;

            for (int i = 0; i < hitCount; i++)
            {
                var enemy = _results[i].GetComponent<IUnit>();

                if (enemy == null || enemy.IsDead)
                    continue;

                float sqrDistance = transform.position.SqrMagnitudeTo(enemy.Transform.position);

                if (sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    _currentUnit = enemy;
                }
            }

            return _currentUnit;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, FinderRadius);
        }
    }
}