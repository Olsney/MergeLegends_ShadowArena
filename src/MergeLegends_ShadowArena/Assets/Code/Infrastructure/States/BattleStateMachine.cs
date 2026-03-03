using Code.Logic;

namespace Code.Infrastructure.States
{
    public class BattleStateMachine : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IBattleService _battleService;

        public BattleStateMachine(GameStateMachine gameStateMachine, IBattleService battleService)
        {
            _gameStateMachine = gameStateMachine;
            _battleService = battleService;
        }

        public void Enter()
            => EnterPlanning();

        public void Exit()
        {
        }

        public void EnterFight()
        {
            if (_battleService.CanStartFight())
                _gameStateMachine.Enter<BattleFightState>();
        }

        private void EnterPlanning()
            => _gameStateMachine.Enter<BattlePlanningState>();
    }
}