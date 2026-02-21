using System;

namespace Code.Logic.PlayerUnitSpawn
{
    public class PlayerSpawnerService : IDisposable, IPlayerSpawnerService
    {
        private readonly IPlayerUnitSpawner playerUnitSpawner;
        private IPlayerUnitSpawnView _view;

        public PlayerSpawnerService(IPlayerUnitSpawner playerUnitSpawner)
        {
            this.playerUnitSpawner = playerUnitSpawner;
        }

        public void InitializeView(IPlayerUnitSpawnView view)
        {
            _view = view;
            _view.SpawnClicked += playerUnitSpawner.Spawn;
        }

        public void Dispose()
        {
            _view.SpawnClicked -= playerUnitSpawner.Spawn;
        }
    }
}