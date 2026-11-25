using Code.Units.PlayerUnits;

namespace Code.Infrastructure.States
{
    public class BattleStateMachine : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPlayerUnitsProvider _playerUnitsProvider;

        public BattleStateMachine(GameStateMachine gameStateMachine, IPlayerUnitsProvider playerUnitsProvider)
        {
            _gameStateMachine = gameStateMachine;
            _playerUnitsProvider = playerUnitsProvider;
        }

        public void Enter()
            => EnterPlanning();

        public void Exit()
        {
        }

        public void EnterFight()
        {
            if (_playerUnitsProvider.GetPlayerUnits().Count > 0)
                _gameStateMachine.Enter<BattleFightState>();
        }

        private void EnterPlanning()
            => _gameStateMachine.Enter<BattlePlanningState>();
    }
}