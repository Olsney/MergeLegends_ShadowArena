namespace Code.Infrastructure.States
{
    public class BattleSpawnEnemiesState : IState
    {
        //private readonly IEnemySpawnerService _enemySpawner;
        private readonly BattleStateMachine _battleStateMachine;

        public BattleSpawnEnemiesState(
            //IEnemySpawnerService enemySpawner,
            BattleStateMachine battleStateMachine)
        {
            //_enemySpawner = enemySpawner;
            _battleStateMachine = battleStateMachine;
        }

        public void Enter()
        {
            //_enemySpawner.SpawnEnemies();
            _battleStateMachine.EnterPlanning();
        }

        public void Exit()
        {
        }
    }
}