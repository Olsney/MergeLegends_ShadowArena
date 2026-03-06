using Code.Units.Attack;
using UnityEngine;
using Zenject;

namespace Code.Units.PlayerUnits
{
    public class PlayerUnit : MonoBehaviour
    {
        public bool IsDead { get; private set; }
        public UnitNavmeshAgentMover UnitNavmeshAgentMover { get; private set; }
        public IAttacker Attacker { get; private set; }

        private UnitDeath _unitDeath;
        private IPlayerUnitProvider _playerUnitProvider;

        [Inject]
        private void Construct(IPlayerUnitProvider playerUnitProvider)
            => _playerUnitProvider = playerUnitProvider;

        private void Awake()
        {
            UnitNavmeshAgentMover = GetComponent<UnitNavmeshAgentMover>();
            Attacker = GetComponentInChildren<IAttacker>();
            _unitDeath = GetComponent<UnitDeath>();
        }

        private void Start()
        {
            if (_unitDeath != null)
                _unitDeath.Dead += OnDead;

            _playerUnitProvider?.AddUnit(this);
        }

        private void OnDestroy()
        {
            if (_unitDeath != null)
                _unitDeath.Dead -= OnDead;

            _playerUnitProvider?.RemoveUnit(this);
        }

        private void OnDead() 
            => IsDead = true;
    }
}