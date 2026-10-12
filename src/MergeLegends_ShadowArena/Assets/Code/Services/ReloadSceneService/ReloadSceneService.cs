using Code.Infrastructure.States;
using Code.Logic.PlayArea.Cell;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.StaticData;
using Code.Units.Enemies;
using Code.Units.PlayerUnits;

namespace Code.Services.ReloadSceneService
{
    public class ReloadSceneService : IReloadSceneService
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPlayerUnitCellsProvider _playerUnitCellsProvider;
        private readonly IEnemyCellsProvider _enemiesCellsProvider;
        private readonly IPlayerUnitsProvider _playerUnitsProvider;
        private readonly IEnemiesProvider _enemiesProvider;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;

        public ReloadSceneService(GameStateMachine gameStateMachine,
            IPlayerUnitCellsProvider playerUnitCellsProvider,
            IEnemyCellsProvider enemyCellsProvider,
            IPlayerUnitsProvider playerUnitsProvider,
            IEnemiesProvider enemiesProvider,
            IPersistentProgressService progressService,
            IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _playerUnitCellsProvider = playerUnitCellsProvider;
            _enemiesCellsProvider = enemyCellsProvider;
            _playerUnitsProvider = playerUnitsProvider;
            _enemiesProvider = enemiesProvider;
            _progressService = progressService;
            _staticData = staticData;
        }

        public void StartReload()
        {
            _playerUnitCellsProvider.Clear();
            _enemiesCellsProvider.Clear();
            _playerUnitsProvider.Clear();
            _enemiesProvider.Clear();

            int levelIndex = _progressService.Progress.WorldData.LevelData.CurrentLevelIndex;
            LocationTypeId locationTypeId = _staticData.GetGameStaticData().GameLevelsData[levelIndex].Location;
            string sceneName = _staticData.GetLocation(locationTypeId).SceneName;

            _gameStateMachine.Enter<LoadLevelState, string>(sceneName);
        }
    }
}