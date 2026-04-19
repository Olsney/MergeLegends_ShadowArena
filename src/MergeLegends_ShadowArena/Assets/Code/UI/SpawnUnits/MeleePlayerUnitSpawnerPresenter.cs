namespace Code.UI.SpawnUnits
{
    public class MeleePlayerUnitSpawnerPresenter : Presenter<SpawnPlayerUnitView>
    {
        private readonly IPlayerUnitSpawner _spawner;

        public MeleePlayerUnitSpawnerPresenter(SpawnPlayerUnitView view, IPlayerUnitSpawner spawner) : base(view)
        {
            _spawner = spawner;
        }

        public override void Initialize()
            => View.SpawnClicked += _spawner.Melee;

        public override void Dispose()
            => View.SpawnClicked -= _spawner.Melee;

        public void RemoveView()
            => View.Remove();
    }
}