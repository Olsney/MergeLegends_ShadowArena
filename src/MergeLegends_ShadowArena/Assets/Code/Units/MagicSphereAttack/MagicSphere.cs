using System.Linq;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Units.MagicSphereAttack
{
    public class MagicSphere : MonoBehaviour
    {
        private const float LifeTime = 3f;
        private const float HitRadius = 1f;
        private const float ZeroPositionY = 0f;

        private UnitDeath _unitDeath;
        private MagicSpherePassiveEffect _passiveEffect;
        private MagicSphereExplosionEffect _explosionEffect;

        private float _speed;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _isActive;
        private float _currentLifeTime;
        private float _damage;
        private Transform _targetTransform;
        private Vector3 _lastDirection;
        private Vector3 _direction;

        private void Awake()
        {
            _passiveEffect = GetComponentInChildren<MagicSpherePassiveEffect>();
            _explosionEffect = GetComponentInChildren<MagicSphereExplosionEffect>();
            gameObject.SetActive(false);
        }

        public void Init(float damage, LayerMaskTypeId targetLayerMask, float speed, UnitDeath unitDeath)
        {
            _damage = damage;
            _layerMask = 1 << (int)targetLayerMask;
            _speed = speed;

            _unitDeath = unitDeath;
            _unitDeath.Dead += OnDestroySelf;
        }

        public void Tick()
        {
            if (_isActive == false)
                return;

            Move();
            UpdateLifeTime();

            if (_currentLifeTime <= 0f)
            {
                StartExplosionAsync();
                return;
            }

            Hit();
        }

        private void Move()
        {
            if (_targetTransform != null)
            {
                _direction = (_targetTransform.position - transform.position).normalized;
                _lastDirection = _direction;
            }
            else
            {
                _direction = _lastDirection;
            }
    
            _direction.y = ZeroPositionY;
            transform.position += _direction * _speed * Time.deltaTime;
        }

        public void Launch(Vector3 position, Transform targetTransform)
        {
            transform.position = new Vector3(position.x, position.y, position.z);
            _targetTransform = targetTransform;
            _lastDirection = (targetTransform.position - position).normalized;
            _currentLifeTime = LifeTime;
            _isActive = true;
            gameObject.SetActive(true);
            _passiveEffect.Play();
        }

        public void ResetTargetTransform() 
            => _targetTransform = null;

        private void Hit()
        {
            if (CanHit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
                StartExplosionAsync();
            }
        }

        private bool CanHit(out Collider hit)
        {
            int hitAmount = Physics.OverlapSphereNonAlloc(transform.position, HitRadius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitAmount > 0;
        }

        private async void StartExplosionAsync()
        {
            _targetTransform = null;
            _isActive = false;
            _passiveEffect.Stop();
            _explosionEffect.Play();

            await UniTask.WaitForSeconds(_explosionEffect.GetDuration());

            if (this != null)
            {
                _explosionEffect.Stop();
                Deactivate();
            }
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

        private void OnDestroySelf()
        {
            _unitDeath.Dead -= OnDestroySelf;
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, HitRadius);
        }
    }
}