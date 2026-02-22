using System;
using Code.UI.Factory;
using Zenject;

namespace Code.Logic.PlayerUnitSpawn
{
    public class PlayerSpawnerService : IInitializable, IDisposable, IPlayerSpawnerService
    {
        private readonly IPlayerUnitSpawner _playerUnitSpawner;
        private readonly IUIFactory _uiFactory;
        private IPlayerUnitSpawnView _view;

        public PlayerSpawnerService(IPlayerUnitSpawner playerUnitSpawner, IUIFactory uiFactory)
        {
            _playerUnitSpawner = playerUnitSpawner;
            _uiFactory = uiFactory;
        }

        public void Initialize()
        {
            _uiFactory.UiRootCreated += InitializeView;
        }

        public void Dispose()
        {
            _view.SpawnClicked -= _playerUnitSpawner.Spawn;
            _uiFactory.UiRootCreated -= InitializeView;
        }

        public void InitializeView()
        {
            _view = _uiFactory.CreateSpawnPlayerUnitView().GetComponent<IPlayerUnitSpawnView>();
            _view.SpawnClicked += _playerUnitSpawner.Spawn;
        }
    }
}