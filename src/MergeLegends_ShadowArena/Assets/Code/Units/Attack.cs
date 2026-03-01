using System.Linq;
using Code.Units;
using UnityEngine;

    public class Attack : MonoBehaviour
    {
        public Transform TargetTransform;
        public float AttackCooldown = 3.0f;
        public float Cleavage = 0.5f;
        public float Damage = 10;

        private UnitAnimator _animator;
        private AnimationEventsReader _animationEventsReader;
        private Collider[] _hits = new Collider[1];
        private int _layerMask;
        private float _attackCooldown;
        private bool _isAttacking;
        private bool _attackIsActive;

        private void Awake()
        {
            _animationEventsReader = GetComponentInParent<AnimationEventsReader>();
            _animator = GetComponentInParent<UnitAnimator>();
            _layerMask = 1 << 7;
        }

        private void Start()
        {
            _animationEventsReader.Attacked += OnAttack;
            _animationEventsReader.AttackEnded += OnAttackEnded;
        }

        private void OnDestroy()
        {
            _animationEventsReader.Attacked -= OnAttack;
            _animationEventsReader.AttackEnded -= OnAttackEnded;
        }

        private void Update()
        {
            UpdateCooldown();
        
            if(CanAttack())
                StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                Debug.Log(hit.name);
                //hit.transform.GetComponent<>().TakeDamage(Damage);
            }
        }

        private void OnAttackEnded()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        }

        public void DisableAttack() => 
            _attackIsActive = false;

        public void EnableAttack() => 
            _attackIsActive = true;

        private bool CooldownIsUp() => 
            _attackCooldown <= 0f;

        private void UpdateCooldown()
        {
            if (CooldownIsUp() == false)
                _attackCooldown -= Time.deltaTime;
        }

        private bool Hit(out Collider hit)
        {
            var hitAmount = Physics.OverlapSphereNonAlloc(transform.position, Cleavage, _hits, _layerMask);
            
            hit = _hits.FirstOrDefault();
      
            return hitAmount > 0;
        }

        private bool CanAttack() => 
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private void StartAttack()
        {
            transform.LookAt(TargetTransform);
            _animator.PlayAttack();
            _isAttacking = true;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Cleavage);
        }
    }
