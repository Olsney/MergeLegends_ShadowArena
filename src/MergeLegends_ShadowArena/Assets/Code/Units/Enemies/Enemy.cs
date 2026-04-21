using Code.Units.Attack;
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

        [Inject]
        private void Construct(IEnemiesProvider enemiesProvider)
            => _enemiesProvider = enemiesProvider;

        private void Awake()
        {
            _unitNavmeshAgentMover = GetComponent<UnitNavmeshAgentMover>();
            _attacker = GetComponentInChildren<IAttacker>();
            _unitDeath = GetComponent<UnitDeath>();
        }

        private void Start()
        {
            _enemiesProvider.AddUnit(this);
            _unitDeath.Dead += OnDead;
        }

        private void OnDestroy()
        {
            _enemiesProvider.RemoveUnit(this);
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