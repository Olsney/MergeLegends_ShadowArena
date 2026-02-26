namespace Code.Infrastructure.States
{
    public class BattleFightState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly BattleStateMachine _battleStateMachine;

        public BattleFightState(GameStateMachine stateMachine, BattleStateMachine battleStateMachine)
        {
            _stateMachine = stateMachine;
            _battleStateMachine = battleStateMachine;
        }

        public void Enter()
        {
            _battleStateMachine.OnFightStarted();
            _battleStateMachine.RemovePlayZone();
            _stateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}