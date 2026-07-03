using Code.Logic.BattleServices;
using Code.Services.CameraMoves;

namespace Code.Infrastructure.States
{
    public class BattleFightState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IBattleService _battleService;
        private readonly ICameraMoverService _cameraMover;

        public BattleFightState(GameStateMachine stateMachine, IBattleService battleService,
            ICameraMoverService cameraMover)
        {
            _stateMachine = stateMachine;
            _battleService = battleService;
            _cameraMover = cameraMover;
        }

        public void Enter()
        {
            _cameraMover.StartMoveToFight();
            _battleService.StartFight();
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}