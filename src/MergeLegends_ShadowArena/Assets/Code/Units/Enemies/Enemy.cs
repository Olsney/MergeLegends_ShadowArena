using Code.Units.Attack;
using Code.Units.UI;
using UnityEngine;
using Zenject;

namespace Code.Units.Enemies
{
    public class Enemy : MonoBehaviour, IUnit
    {
        public bool IsDead { get; private set; }
        public Transform Transform => transform;

        private UnitNavmeshAgentMover _unitNavmeshAgentMover;
        private IAttacker _attacker;
        private UnitDeath _unitDeath;
        private IEnemiesProvider _enemiesProvider;
        private EnemyHealth _enemyHealth;

        private SpawnEffect _spawnEffect;
        private DamageEffect _damageEffect;

        [Inject]
        private void Construct(IEnemiesProvider enemiesProvider)
            => _enemiesProvider = enemiesProvider;

        private void Awake()
        {
            _unitNavmeshAgentMover = GetComponent<UnitNavmeshAgentMover>();
            _attacker = GetComponentInChildren<IAttacker>();
            _unitDeath = GetComponent<UnitDeath>();
            _enemyHealth = GetComponent<EnemyHealth>();
            
            _spawnEffect = GetComponentInChildren<SpawnEffect>();
            _damageEffect = GetComponentInChildren<DamageEffect>();
        }

        private void Start()
        {
            _enemiesProvider.AddUnit(this);
            _unitDeath.Dead += OnDead;
            _enemyHealth.HealthChanged += _damageEffect.Play;
            _spawnEffect.Play();
        }

        private void OnDestroy()
        {
            _enemiesProvider.RemoveUnit(this);
            _enemyHealth.HealthChanged -= _damageEffect.Play;
            _unitDeath.Dead -= OnDead;
        }

        public void ProcessUnit()
        {
            if (IsDead)
                return;

            _unitNavmeshAgentMover.StartMoving();
            _attacker.StartAttacking();
        }

        private void OnDead()
            => IsDead = true;
    }
}