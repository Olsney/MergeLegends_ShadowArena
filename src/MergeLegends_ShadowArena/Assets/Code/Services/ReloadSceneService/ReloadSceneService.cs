using Code.Infrastructure.States;
using Code.Logic.PlayArea.Cell;
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

        public ReloadSceneService(GameStateMachine gameStateMachine,
            IPlayerUnitCellsProvider playerUnitCellsProvider,
            IEnemyCellsProvider enemyCellsProvider,
            IPlayerUnitsProvider playerUnitsProvider,
            IEnemiesProvider enemiesProvider)
        {
            _gameStateMachine = gameStateMachine;
            _playerUnitCellsProvider = playerUnitCellsProvider;
            _enemiesCellsProvider = enemyCellsProvider;
            _playerUnitsProvider = playerUnitsProvider;
            _enemiesProvider = enemiesProvider;
        }

        public void StartReload()
        {
            _playerUnitCellsProvider.Clear();
            _enemiesCellsProvider.Clear();
            _playerUnitsProvider.Clear();
            _enemiesProvider.Clear();
            
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}