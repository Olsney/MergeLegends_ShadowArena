using System.Collections;
using System.Linq;
using Code.Units.Config;
using Code.Units.PlayerUnit.Config;
using UnityEngine;

namespace Code.Units.MagicSphereAttack
{
    public class MagicSphere : MonoBehaviour
    {
        private const float Speed = 7f;
        private const float LifeTime = 3f;
        private const float HitRadius = 0.25f;

        private MagicSpherePassiveEffect _passiveEffect;
        private MagicSphereExplosionEffect _explosionEffect;

        private Coroutine _coroutine;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _isActive;
        private float _currentLifeTime;
        private float _damage;

        private void Awake()
        {
            _passiveEffect = GetComponentInChildren<MagicSpherePassiveEffect>();
            _explosionEffect = GetComponentInChildren<MagicSphereExplosionEffect>();
            gameObject.SetActive(false);
        }

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
            _passiveEffect.Play();
        }

        private void Hit()
        {
            if (TryHit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
                StartExplosionCoroutine();
            }
        }

        private bool TryHit(out Collider hit)
        {
            int hitAmount = Physics.OverlapSphereNonAlloc(transform.position, HitRadius, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitAmount > 0;
        }

        private IEnumerator PlayExplosionAndDeactivate()
        {
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
            // Останавливаем предыдущую корутину, если она была
            StopExplosionCoroutine();

            // Запускаем новую
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