namespace Code.Infrastructure.States
{
    public class BattleFightState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public BattleFightState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}