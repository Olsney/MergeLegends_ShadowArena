using System.Collections;
using System.Linq;
using Code.StaticData;
using UnityEngine;

namespace Code.Units.MagicSphereAttack
{
    public class MagicSphere : MonoBehaviour
    {
        private const float LifeTime = 3f;
        private const float HitRadius = 0.25f;
        private const float ZeroPositionY = 0f;

        private MagicSpherePassiveEffect _passiveEffect;
        private MagicSphereExplosionEffect _explosionEffect;

        private float _speed;
        private Coroutine _coroutine;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _isActive;
        private float _currentLifeTime;
        private float _damage;
        private Transform _targetTransform;

        private void Awake()
        {
            _passiveEffect = GetComponentInChildren<MagicSpherePassiveEffect>();
            _explosionEffect = GetComponentInChildren<MagicSphereExplosionEffect>();
            gameObject.SetActive(false);
        }

        public void Init(float damage, LayerMaskTypeId targetLayerMask, float speed)
        {
            _damage = damage;
            _layerMask = 1 << (int)targetLayerMask;
            _speed = speed;
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
        {
            Vector3 direction = (_targetTransform.position - transform.position).normalized;
            direction.y = ZeroPositionY;
            transform.position += direction * _speed * Time.deltaTime;
        }

        public void Launch(Vector3 position, Transform targetTransform)
        {
            transform.position = new Vector3(position.x, position.y, position.z);
            _targetTransform =  targetTransform;
            _currentLifeTime = LifeTime;
            _isActive = true;
            gameObject.SetActive(true);
            _passiveEffect.Play();
        }

        private void Hit()
        {
            if (CanHit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
                StartExplosionCoroutine();
            }
        }

        private bool CanHit(out Collider hit)
        {
            int hitAmount = Physics.OverlapSphereNonAlloc(transform.position, HitRadius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitAmount > 0;
        }

        private IEnumerator PlayExplosionAndDeactivate()
        {
            _targetTransform = null;
            _isActive = false;
            _passiveEffect.Stop();
            _explosionEffect.Play();

            yield return new WaitForSeconds(_explosionEffect.GetDuration());

            Deactivate();
        }

        private void Deactivate()
        {
            StopExplosionCoroutine();
            _isActive = false;
            gameObject.SetActive(false);
        }

        private void UpdateLifeTime()
        {
            if (_currentLifeTime <= 0f == false)
                _currentLifeTime -= Time.deltaTime;
        }

        private void StartExplosionCoroutine()
        {
            StopExplosionCoroutine();

            _coroutine = StartCoroutine(PlayExplosionAndDeactivate());
        }

        private void StopExplosionCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, HitRadius);
        }
    }
}