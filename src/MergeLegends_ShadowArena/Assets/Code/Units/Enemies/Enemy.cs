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
        private LookAtCanvasToCamera _lookAtCanvasToCamera;

        [Inject]
        private void Construct(IEnemiesProvider enemiesProvider)
            => _enemiesProvider = enemiesProvider;

        private void Awake()
        {
            _unitNavmeshAgentMover = GetComponent<UnitNavmeshAgentMover>();
            _attacker = GetComponentInChildren<IAttacker>();
            _unitDeath = GetComponent<UnitDeath>();
            _lookAtCanvasToCamera = GetComponentInChildren<LookAtCanvasToCamera>();
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

        public void Tick()
        {
            _unitNavmeshAgentMover.Tick();
            _attacker.Tick();
            _lookAtCanvasToCamera.Tick();
        }

        private void OnDead()
            => IsDead = true;
    }
}