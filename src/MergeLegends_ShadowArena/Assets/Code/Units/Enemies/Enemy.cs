using System;
using Code.Units.Attack;
using Code.Units.Effects;
using UnityEngine;
using Zenject;

namespace Code.Units.Enemies
{
    public class Enemy : MonoBehaviour, IUnit
    {
        public UnitDeath UnitDeath { get; private set; }
        public UnitEffects Effects { get; private set; }
        public bool IsDead { get; private set; }
        public Transform Transform => transform;

        public event Action Destroying;

        private UnitNavmeshAgentMover _unitNavmeshAgentMover;
        private IAttacker _attacker;
        private IEnemiesProvider _enemiesProvider;
        private EnemyHealth _enemyHealth;
        private UnitAnimator _unitAnimator;

        [Inject]
        private void Construct(IEnemiesProvider enemiesProvider)
            => _enemiesProvider = enemiesProvider;

        private void Awake()
        {
            UnitDeath = GetComponent<UnitDeath>();
            Effects = GetComponent<UnitEffects>();
            
            _unitNavmeshAgentMover = GetComponent<UnitNavmeshAgentMover>();
            _enemyHealth = GetComponent<EnemyHealth>();
            
            _attacker = GetComponentInChildren<IAttacker>();
            _unitAnimator = GetComponentInChildren<UnitAnimator>();
        }

        private void Start()
        {
            _enemiesProvider.AddUnit(this);
            UnitDeath.Dead += OnDead;
            _enemyHealth.HealthChanged += OnHealthChanged;
            Effects.Play(UnitEffectTypeId.Spawn);
        }

        private void OnDestroy()
        {
            Destroying?.Invoke();
            _enemiesProvider.RemoveUnit(this);
            _enemyHealth.HealthChanged -= OnHealthChanged;
            UnitDeath.Dead -= OnDead;
        }

        public void ProcessUnit()
        {
            if (IsDead)
                return;

            _unitNavmeshAgentMover.StartMoving();
            _attacker.StartAttacking();
        }

        public void StartVictoryAnimation()
        {
            _unitNavmeshAgentMover.DeactivateMoving();
            _unitAnimator.PlayVictory();
        }
        
        private void OnHealthChanged()
            => Effects.Play(UnitEffectTypeId.Damage);

        private void OnDead()
        {
            UnitDeath.Dead -= OnDead;
            _enemiesProvider.RemoveUnit(this);
            IsDead = true;
        }
    }
}