namespace Code.Infrastructure.States
{
    public class BattleStateMachine : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public BattleStateMachine(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
            => EnterSpawnEnemies();

        public void Exit()
        {
        }

        public void EnterPlanning() => _gameStateMachine.Enter<BattlePlanningState>();
        public void EnterFight() => _gameStateMachine.Enter<BattleFightState>();
        private void EnterSpawnEnemies() => _gameStateMachine.Enter<BattleSpawnEnemiesState>();
    }
}