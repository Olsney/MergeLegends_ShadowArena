using System.Linq;
using Code.Infrastructure.Builders;
using Code.Logic.PlayArea.Cell;
using Code.Services.PersistentProgress;
using Code.StaticData;

namespace Code.Services.CellsInitializerServices
{
    public class CellsInitializerService : ICellsInitializerService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IUnitBuilder _unitBuilder;
        private readonly IPlayerUnitCellsProvider _playerUnitCellsProvider;
        private readonly IEnemyCellsProvider _enemyCellsProvider;

        public CellsInitializerService(
            IPersistentProgressService progressService,
            IUnitBuilder unitBuilder,
            IPlayerUnitCellsProvider playerUnitCellsProvider,
            IEnemyCellsProvider enemyCellsProvider)
        {
            _progressService = progressService;
            _unitBuilder = unitBuilder;
            _playerUnitCellsProvider = playerUnitCellsProvider;
            _enemyCellsProvider = enemyCellsProvider;
        }

        public void InitializeAllCells()
        {
            InitializePlayerUnits();
            InitializeEnemyUnits();
        }

        private void InitializePlayerUnits()
        {
            var allCells = _playerUnitCellsProvider.GetAllCells().ToList();

            for (int i = 0; i < allCells.Count; i++)
            {
                if (allCells[i] is CellGridPlayerUnit cellGridPlayerUnit)
                {
                    var savedData = _progressService.Progress.WorldData.CompositionTeamData.PlayerUnitInCells
                        .FirstOrDefault(data => data.CellIndex == i);

                    if (savedData != null)
                    {
                        var unit = _unitBuilder
                            .SetUnitType(UnitTypeId.PlayerUnit)
                            .SetUnitAttackType(savedData.UnitAttackTypeId)
                            .SetCurrentLevel(savedData.PlayerUnitLevelIndex)
                            .SetPosition(cellGridPlayerUnit.Transform.position)
                            .Build();

                        cellGridPlayerUnit.Init(i, unit);
                    }
                    else
                    {
                        cellGridPlayerUnit.Init(i);
                    }
                }
            }
        }

        private void InitializeEnemyUnits()
        {
            var allCells = _enemyCellsProvider.GetAllCells().ToList();

            for (int i = 0; i < allCells.Count; i++)
            {
                var cell = allCells[i];

                if (cell is CellGridEnemy cellGridEnemy)
                {
                    cellGridEnemy.Init(i);
                }
            }
        }
    }
}