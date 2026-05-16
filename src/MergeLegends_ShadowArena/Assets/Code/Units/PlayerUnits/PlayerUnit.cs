using System;
using Code.Services.MergeUnits;
using Code.StaticData;
using Code.Units.Attack;
using UnityEngine;
using Zenject;

namespace Code.Units.PlayerUnits
{
    public class PlayerUnit : MonoBehaviour, IUnit
    {
        public UnitDeath UnitDeath { get; private set; }
        public bool IsDead { get; private set; }
        public Transform Transform => transform;
        public int LevelIndex { get; private set; }
        public UnitAttackTypeId AttackType { get; private set; }

        public event Action Destroying;
        public event Action ProcessStarting;

        private UnitNavmeshAgentMover _unitNavmeshAgentMover;
        private IAttacker _attacker;
        private IPlayerUnitsProvider _playerUnitsProvider;
        private PlayerUnitHealth _playerUnitHealth;
        private UnitAnimator _unitAnimator;

        private SpawnEffect _spawnEffect;
        private LevelUpEffect _levelUpEffect;
        private DamageEffect _damageEffect;

        [Inject]
        private void Construct(IPlayerUnitsProvider playerUnitsProvider)
            => _playerUnitsProvider = playerUnitsProvider;

        public void Init(int unitCurrentLevelIndex, UnitAttackTypeId attackTypeId)
        {
            LevelIndex = unitCurrentLevelIndex;
            AttackType = attackTypeId;
        }

        public void StartLevelUpEffect()
            => _levelUpEffect.Play();

        private void Awake()
        {
            UnitDeath = GetComponent<UnitDeath>();
            _unitNavmeshAgentMover = GetComponent<UnitNavmeshAgentMover>();
            _playerUnitHealth = GetComponent<PlayerUnitHealth>();
            
            _unitAnimator = GetComponentInChildren<UnitAnimator>();
            _attacker = GetComponentInChildren<IAttacker>();
            
            _damageEffect = GetComponentInChildren<DamageEffect>();
            _levelUpEffect = GetComponentInChildren<LevelUpEffect>();
            _spawnEffect = GetComponentInChildren<SpawnEffect>();
        }

        private void Start()
        {
            _playerUnitsProvider.AddUnit(this);
            UnitDeath.Dead += OnDead;
            _playerUnitHealth.HealthChanged += _damageEffect.Play;
            _spawnEffect.Play();
        }

        private void OnDestroy()
        {
            Destroying?.Invoke();
            _playerUnitsProvider.RemoveUnit(this);
            UnitDeath.Dead -= OnDead;
            _playerUnitHealth.HealthChanged -= _damageEffect.Play;
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

        public void Remove()
            => Destroy(gameObject);

        private void OnDead()
        {
            UnitDeath.Dead -= OnDead;
            _playerUnitsProvider.RemoveUnit(this);
            IsDead = true;
        }
    }
}