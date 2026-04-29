using Code.Services.MergeUnits;
using Code.StaticData;
using Code.Units.Attack;
using Code.Units.UI;
using UnityEngine;
using Zenject;

namespace Code.Units.PlayerUnits
{
    public class PlayerUnit : MonoBehaviour, IUnit
    {
        public bool IsDead { get; private set; }
        public Transform Transform => transform;
        public int LevelIndex { get; private set; }
        public UnitAttackTypeId AttackType { get; private set; }

        private UnitNavmeshAgentMover _unitNavmeshAgentMover;
        private IAttacker _attacker;
        private UnitDeath _unitDeath;
        private IPlayerUnitsProvider _playerUnitsProvider;
        private PlayerUnitHealth _playerUnitHealth;
        
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
            _unitNavmeshAgentMover = GetComponent<UnitNavmeshAgentMover>();
            _attacker = GetComponentInChildren<IAttacker>();
            _unitDeath = GetComponent<UnitDeath>();
            _spawnEffect = GetComponentInChildren<SpawnEffect>();
            _levelUpEffect = GetComponentInChildren<LevelUpEffect>();
            _playerUnitHealth = GetComponent<PlayerUnitHealth>();
            _damageEffect = GetComponentInChildren<DamageEffect>();
        }

        private void Start()
        {
            _playerUnitsProvider.AddUnit(this);
            _unitDeath.Dead += OnDead;
            _playerUnitHealth.HealthChanged += _damageEffect.Play;
            _spawnEffect.Play();
        }

        private void OnDestroy()
        {
            _playerUnitsProvider.RemoveUnit(this);
            _unitDeath.Dead -= OnDead;
            _playerUnitHealth.HealthChanged -= _damageEffect.Play;
        }

        public void ProcessUnit()
        {
            if (IsDead)
                return;

            _unitNavmeshAgentMover.StartMoving();
            _attacker.StartAttacking();
        }

        public void Remove()
            => Destroy(gameObject);

        private void OnDead()
            => IsDead = true;
    }
}