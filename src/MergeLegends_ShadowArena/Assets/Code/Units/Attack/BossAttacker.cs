using Code.Services.CameraMoves;
using Code.StaticData;
using Code.Units.Audio;
using UnityEngine;
using Zenject;

namespace Code.Units.Attack
{
    public class BossAttacker : MonoBehaviour, IAttacker
    {
        [SerializeField] private float _hitRadius = 2f;

        private const int MaxHits = 15;

        public bool IsAttacking { get; private set; }

        private ICameraShakeService _cameraShakeService;
        private CameraShakeToAttackData _cameraShakeToAttackData;

        private UnitAnimator _animator;
        private AnimationEventsReader _animationEventsReader;
        private UnitNavmeshAgentMover _navmeshAgentMover;
        private UnitDeath _unitDeath;
        private BossLastAttackEffect bossLastAttackEffect;
        private BossAttackSound _bossAttackSound;
        private BossLastAttackSound _bossLastAttackSound;

        private Collider[] _hits;
        private int _layerMask;
        private float _damage;
        private float _attackCooldown;
        private float _currentAttackCooldown;
        private bool _isActive;

        [Inject]
        private void Construct(ICameraShakeService cameraShakeService)
            => _cameraShakeService = cameraShakeService;

        private void Awake()
        {
            _hits = new Collider[MaxHits];

            _animationEventsReader = GetComponentInParent<AnimationEventsReader>();
            _animator = GetComponentInParent<UnitAnimator>();
            _navmeshAgentMover = GetComponentInParent<UnitNavmeshAgentMover>();
            _unitDeath = GetComponentInParent<UnitDeath>();
            bossLastAttackEffect = GetComponentInChildren<BossLastAttackEffect>();
        }

        private void OnEnable()
        {
            _animationEventsReader.Attacked += OnAttack;
            _animationEventsReader.LastAttacked += OnLastAttack;
            _animationEventsReader.AttackEnded += OnAttackEnded;
            _navmeshAgentMover.TargetReached += StartAttack;
            _unitDeath.Dead += StopAttacking;
        }

        private void OnDisable()
        {
            _animationEventsReader.Attacked -= OnAttack;
            _animationEventsReader.LastAttacked -= OnLastAttack;
            _animationEventsReader.AttackEnded -= OnAttackEnded;
            _navmeshAgentMover.TargetReached -= StartAttack;
            _unitDeath.Dead -= StopAttacking;
        }

        private void Update()
        {
            if (_isActive == false)
                return;

            UpdateCooldown();
        }

        public void Init(float damage, float cooldown, LayerMaskTypeId targetLayerMask,
            CameraShakeToAttackData cameraShakeToAttackData, BossAttackSound bossAttackSound)
        {
            _damage = damage;
            _attackCooldown = cooldown;
            _layerMask = 1 << (int)targetLayerMask;
            _cameraShakeToAttackData = cameraShakeToAttackData;
            _bossAttackSound = bossAttackSound;
        }

        public void StartAttacking()
            => _isActive = true;

        private void StopAttacking()
            => _isActive = false;

        private void OnAttack()
        {
            _bossAttackSound.PlayAttackSound();
            PerformAttack();
        }

        private void OnLastAttack()
        {
            bossLastAttackEffect.Play();
            _cameraShakeService.StartVerticleShake(_cameraShakeToAttackData.Force, _cameraShakeToAttackData.Direction);
            _bossAttackSound.PlayLastAttackSound();
            PerformAttack();
        }

        private void PerformAttack()
        {
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _hitRadius, _hits, _layerMask);

            for (int i = 0; i < hitCount; i++)
            {
                if (_hits[i].TryGetComponent<IHealth>(out var health))
                    health.TakeDamage(_damage);
            }
        }

        private void OnAttackEnded()
        {
            _currentAttackCooldown = _attackCooldown;
            IsAttacking = false;
        }

        private void StartAttack()
        {
            if (CanAttack() == false)
                return;

            _animator.PlayAttack();
            IsAttacking = true;
        }

        private void UpdateCooldown()
        {
            if (_currentAttackCooldown > 0f)
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            IsAttacking == false && _currentAttackCooldown <= 0f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _hitRadius);
        }
    }
}