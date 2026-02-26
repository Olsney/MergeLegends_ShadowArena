using Code.Infrastructure.States;

namespace Code.UI
{
    public class PlayerSpawnerPresenter : Presenter<PlayerUnitSpawnView>
    {
        private readonly IPlayerUnitSpawner _spawner;
        private readonly BattleStateMachine _battleStateMachine;

        public PlayerSpawnerPresenter(PlayerUnitSpawnView view, IPlayerUnitSpawner spawner,
            BattleStateMachine battleStateMachine) : base(view)
        {
            _spawner = spawner;
            _battleStateMachine = battleStateMachine;
        }

        public override void Initialize()
        {
            View.SpawnClicked += _spawner.Spawn;
            _battleStateMachine.FightStarted += RemoveView;
        }

        public override void Dispose()
        {
            View.SpawnClicked -= _spawner.Spawn;
            _battleStateMachine.FightStarted -= RemoveView;
        }

        private void RemoveView()
        {
            View.Remove();
        }
    }
}