using System;
using Code.Units.Attack;
using UnityEngine;
using Zenject;

namespace Code.Units.Enemies
{
    public class Enemy : MonoBehaviour, IUnit
    {
        public UnitDeath UnitDeath { get; private set; }
        public bool IsDead { get; private set; }
        public Transform Transform => transform;

        public event Action Destroying;
        public event Action ProcessStarting;

        private UnitNavmeshAgentMover _unitNavmeshAgentMover;
        private IAttacker _attacker;
        private IEnemiesProvider _enemiesProvider;
        private EnemyHealth _enemyHealth;
        private UnitAnimator _unitAnimator;

        private SpawnEffect _spawnEffect;
        private DamageEffect _damageEffect;

        [Inject]
        private void Construct(IEnemiesProvider enemiesProvider)
            => _enemiesProvider = enemiesProvider;

        private void Awake()
        {
            UnitDeath = GetComponent<UnitDeath>();
            _unitNavmeshAgentMover = GetComponent<UnitNavmeshAgentMover>();
            _enemyHealth = GetComponent<EnemyHealth>();
            
            _attacker = GetComponentInChildren<IAttacker>();
            _unitAnimator = GetComponentInChildren<UnitAnimator>();

            _spawnEffect = GetComponentInChildren<SpawnEffect>();
            _damageEffect = GetComponentInChildren<DamageEffect>();
        }

        private void Start()
        {
            _enemiesProvider.AddUnit(this);
            UnitDeath.Dead += OnDead;
            _enemyHealth.HealthChanged += _damageEffect.Play;
            _spawnEffect.Play();
        }

        private void OnDestroy()
        {
            Destroying?.Invoke();
            _enemiesProvider.RemoveUnit(this);
            _enemyHealth.HealthChanged -= _damageEffect.Play;
            UnitDeath.Dead -= OnDead;
        }

        public void ProcessUnit()
        {
            if (IsDead)
                return;

            ProcessStarting?.Invoke();

            _unitNavmeshAgentMover.StartMoving();
            _attacker.StartAttacking();
        }

        public void StartVictoryAnimation()
        {
            _unitNavmeshAgentMover.DeactivateMoving();
            _unitAnimator.PlayVictory();
        }

        private void OnDead()
        {
            UnitDeath.Dead -= OnDead;
            _enemiesProvider.RemoveUnit(this);
            IsDead = true;
        }
    }
}