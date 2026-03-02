using System.Linq;
using Code.Units.Config;
using Code.Units.PlayerUnit.Config;
using UnityEngine;

namespace Code.Units
{
    public class MagicSphere : MonoBehaviour
    {
        private const float Speed = 7f;
        private const float LifeTime = 3f;
        private const float HitRadius = 0.25f;
    
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _isActive;
        private float _currentLifeTime;
        private float _damage;

        private void Awake() 
            => gameObject.SetActive(false);

        public void InitConfig(TargetConfig targetConfig, UnitConfig unitConfig)
        {
            _damage = unitConfig.Damage;
            _layerMask = 1 << (int)targetConfig.TargetMask;
        }

        public void Tick()
        {
            if (_isActive == false)
                return;

            Move();
            UpdateLifeTime();

            if (_currentLifeTime <= 0f)
            {
                Deactivate();
                return;
            }

            Hit();
        }

        private void Move() 
            => transform.position += transform.forward * Speed * Time.deltaTime;

        public void Launch(Vector3 position, Vector3 direction)
        {
            transform.position = new Vector3(position.x, position.y, position.z);
            transform.forward = direction;
            _currentLifeTime = LifeTime;
            _isActive = true;
            gameObject.SetActive(true);
        }

        private void Hit()
        {
            if (TryHit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
                Deactivate();
            }
        }

        private bool TryHit(out Collider hit)
        {
            int hitAmount = Physics.OverlapSphereNonAlloc(transform.position, HitRadius, _hits, _layerMask);
        
            hit = _hits.FirstOrDefault();

            return hitAmount > 0;
        }

        private void Deactivate()
        {
            _isActive = false;
            gameObject.SetActive(false);
        }
    
        private void UpdateLifeTime()
        {
            if (_currentLifeTime <= 0f == false)
                _currentLifeTime -= Time.deltaTime;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, HitRadius);
        }
    }
}