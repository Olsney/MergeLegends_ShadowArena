using Code.Logic.BattleServices;

namespace Code.Infrastructure.States
{
    public class BattleFightState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IBattleService _battleService;

        public BattleFightState(GameStateMachine stateMachine, IBattleService battleService)
        {
            _stateMachine = stateMachine;
            _battleService = battleService;
        }

        public void Enter()
        {
            _battleService.StartFight();
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}