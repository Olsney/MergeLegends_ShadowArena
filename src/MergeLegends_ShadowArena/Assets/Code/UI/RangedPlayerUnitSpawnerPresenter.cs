namespace Code.UI
{
    public class RangedPlayerUnitSpawnerPresenter : Presenter<PlayerUnitSpawnView>
    {
        private readonly IPlayerUnitSpawner _spawner;

        public RangedPlayerUnitSpawnerPresenter(PlayerUnitSpawnView view, IPlayerUnitSpawner spawner) : base(view)
        {
            _spawner = spawner;
        }

        public override void Initialize()
            => View.SpawnClicked += _spawner.SpawnRanged;

        public override void Dispose()
            => View.SpawnClicked -= _spawner.SpawnRanged;

        public void RemoveView()
            => View.Remove();
    }
}