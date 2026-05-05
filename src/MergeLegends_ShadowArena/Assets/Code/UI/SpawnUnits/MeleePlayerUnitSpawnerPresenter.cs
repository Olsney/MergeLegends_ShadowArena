using Code.StaticData;

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
            => View.SpawnClicked += OnSpawnClicked;

        public override void Dispose()
            => View.SpawnClicked -= OnSpawnClicked;

        private void OnSpawnClicked() 
            => _spawner.Spawn(UnitAttackTypeId.Melee);

        public void RemoveView()
            => View.Remove();
    }
}