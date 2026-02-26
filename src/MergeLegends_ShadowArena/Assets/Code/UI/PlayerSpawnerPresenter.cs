using Code.Infrastructure.States;

namespace Code.UI
{
    public class PlayerSpawnerPresenter : Presenter<PlayerUnitSpawnView>
    {
        private readonly IPlayerUnitSpawner _spawner;

        public PlayerSpawnerPresenter(PlayerUnitSpawnView view, IPlayerUnitSpawner spawner,
            BattleStateMachine battleStateMachine) : base(view)
        {
            _spawner = spawner;
        }

        public override void Initialize()
        {
            View.SpawnClicked += _spawner.Spawn;
        }

        public override void Dispose()
        {
            View.SpawnClicked -= _spawner.Spawn;
        }

        public void RemoveView()
        {
            View.Remove();
        }
    }
}