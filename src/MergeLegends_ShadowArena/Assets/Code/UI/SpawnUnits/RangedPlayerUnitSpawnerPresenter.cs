using Code.StaticData;

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
            => View.SpawnClicked += OnSpawnClicked;

        public override void Dispose()
            => View.SpawnClicked -= OnSpawnClicked;

        private void OnSpawnClicked() 
            => _spawner.Spawn(UnitAttackTypeId.Ranged);

        public void RemoveView()
            => View.Remove();
    }
}