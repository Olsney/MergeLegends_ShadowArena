using Code.Units.Attack;
using UnityEngine;
using Zenject;

namespace Code.Units.PlayerUnits
{
    public class PlayerUnit : MonoBehaviour, IUnit
    {
        public bool IsDead { get; private set; }
        public Transform Transform => transform;

        private UnitNavmeshAgentMover _unitNavmeshAgentMover;
        private IAttacker _attacker;
        private UnitDeath _unitDeath;
        private IPlayerUnitsProvider _playerUnitsProvider;

        [Inject]
        private void Construct(IPlayerUnitsProvider playerUnitsProvider)
            => _playerUnitsProvider = playerUnitsProvider;

        private void Awake()
        {
            _unitNavmeshAgentMover = GetComponent<UnitNavmeshAgentMover>();
            _attacker = GetComponentInChildren<IAttacker>();
            _unitDeath = GetComponent<UnitDeath>();
        }

        private void Start()
        {
            _playerUnitsProvider.AddUnit(this);
            _unitDeath.Dead += OnDead;
        }

        private void OnDestroy()
        {
            _playerUnitsProvider.RemoveUnit(this);
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