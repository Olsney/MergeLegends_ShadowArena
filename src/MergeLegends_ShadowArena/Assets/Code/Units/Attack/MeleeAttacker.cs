using Code.StaticData;
using UnityEngine;

namespace Code.Units.Attack
{
    public class MeleeAttacker : BaseAttacker
    {
        private const float HitRadius = 0.35f;

        private Collider[] _hits = new Collider[1];
        private int _layerMask;
        private float _damage;
        
        public override void InitConfig(float damage, float cooldown, LayerMaskTypeId targetLayerMask)
        {
            _damage = damage;
            _attackCooldown = cooldown;
            _layerMask = 1 << (int)targetLayerMask;
        }

        protected override void OnAttack()
        {
            if (CanHit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
            }
        }

        private bool CanHit(out Collider hit)
        {
            int hitAmount = Physics.OverlapSphereNonAlloc(transform.position, HitRadius, _hits, _layerMask);
            hit = hitAmount > 0 ? _hits[0] : null;
            return hitAmount > 0;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, HitRadius);
        }
    }
}