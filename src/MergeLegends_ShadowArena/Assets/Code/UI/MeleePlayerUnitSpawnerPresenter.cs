namespace Code.UI
{
    public class MeleePlayerUnitSpawnerPresenter : Presenter<PlayerUnitSpawnView>
    {
        private readonly IPlayerUnitSpawner _spawner;

        public MeleePlayerUnitSpawnerPresenter(PlayerUnitSpawnView view, IPlayerUnitSpawner spawner) : base(view)
        {
            _spawner = spawner;
        }

        public override void Initialize()
            => View.SpawnClicked += _spawner.SpawnMelee;

        public override void Dispose()
            => View.SpawnClicked -= _spawner.SpawnMelee;

        public void RemoveView()
            => View.Remove();
    }
}