using System;
using Code.Services.MergeUnits;
using Code.StaticData;
using Code.Units.Attack;
using Code.Units.Effects;
using UnityEngine;
using Zenject;

namespace Code.Units.PlayerUnits
{
    public class PlayerUnit : MonoBehaviour, IUnit
    {
        public UnitDeath UnitDeath { get; private set; }
        public UnitEffects Effects { get; private set; }
        public bool IsDead { get; private set; }
        public Transform Transform => transform;
        public int LevelIndex { get; private set; }
        public UnitAttackTypeId AttackType { get; private set; }

        public event Action Destroying;

        private UnitNavmeshAgentMover _unitNavmeshAgentMover;
        private IAttacker _attacker;
        private IPlayerUnitsProvider _playerUnitsProvider;
        private PlayerUnitHealth _playerUnitHealth;
        private UnitAnimator _unitAnimator;

        [Inject]
        private void Construct(IPlayerUnitsProvider playerUnitsProvider)
            => _playerUnitsProvider = playerUnitsProvider;

        public void Init(int unitCurrentLevelIndex, UnitAttackTypeId attackTypeId)
        {
            LevelIndex = unitCurrentLevelIndex;
            AttackType = attackTypeId;
        }

        private void Awake()
        {
            UnitDeath = GetComponent<UnitDeath>();
            Effects = GetComponent<UnitEffects>();

            _unitNavmeshAgentMover = GetComponent<UnitNavmeshAgentMover>();
            _playerUnitHealth = GetComponent<PlayerUnitHealth>();

            _unitAnimator = GetComponentInChildren<UnitAnimator>();
            _attacker = GetComponentInChildren<IAttacker>();
        }

        private void Start()
        {
            _playerUnitsProvider.AddUnit(this);
            UnitDeath.Dead += OnDead;
            _playerUnitHealth.HealthChanged += OnHealthChanged;
            Effects.Play(UnitEffectTypeId.Spawn);
        }

        private void OnDestroy()
        {
            Destroying?.Invoke();
            _playerUnitsProvider.RemoveUnit(this);
            UnitDeath.Dead -= OnDead;
            _playerUnitHealth.HealthChanged -= OnHealthChanged;
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

        public void Remove()
            => Destroy(gameObject);

        private void OnHealthChanged()
            => Effects.Play(UnitEffectTypeId.Damage);

        private void OnDead()
        {
            UnitDeath.Dead -= OnDead;
            _playerUnitsProvider.RemoveUnit(this);
            IsDead = true;
        }
    }
}