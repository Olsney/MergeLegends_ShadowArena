namespace Code.UI.SpawnUnits
{
    public class RangedPlayerUnitSpawnerPresenter : Presenter<SpawnPlayerUnitView>
    {
        private readonly IPlayerUnitSpawner _spawner;

        public RangedPlayerUnitSpawnerPresenter(SpawnPlayerUnitView view, IPlayerUnitSpawner spawner) : base(view)
        {
            _spawner = spawner;
        }

        public override void Initialize()
            => View.SpawnClicked += _spawner.Ranged;

        public override void Dispose()
            => View.SpawnClicked -= _spawner.Ranged;

        public void RemoveView()
            => View.Remove();
    }
}