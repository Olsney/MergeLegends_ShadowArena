using System;
using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.Builders;
using Code.Logic.PlayArea.Cell;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.StaticData;

namespace Code.Logic.EnemySpawners
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly IUnitBuilder _unitBuilder;
        private readonly IEnemyCellsProvider _cellsProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _progressService;

        public EnemySpawner(
            IUnitBuilder unitBuilder,
            IEnemyCellsProvider cellsProvider,
            IStaticDataService staticDataService,
            IPersistentProgressService progressService)
        {
            _unitBuilder = unitBuilder;
            _cellsProvider = cellsProvider;
            _staticDataService = staticDataService;
            _progressService = progressService;
        }

        public void Spawn()
        {
            var levelIndex = _progressService.Progress.WorldData.LevelData.CurrentLevelIndex;
            var enemiesSpawnData = _staticDataService.GetGameStaticData()
                .GameLevelsData[levelIndex].EnemiesSpawnData;
            var allCells = _cellsProvider.GetAllCells().ToList();
            var freeCells = allCells.Where(c => !c.IsOccupied).ToList();

            SpawnFixedCells(enemiesSpawnData, allCells, freeCells);
            SpawnRandomCells(enemiesSpawnData, freeCells);
        }

        private void SpawnFixedCells(IReadOnlyList<EnemySpawnData> data, List<ICellGrid> allCells,
            List<ICellGrid> freeCells)
        {
            for (int i = 0; i < data.Count; i++)
            {
                var config = data[i];
                if (config.CellIndex < 0) continue;

                var cell = allCells.FirstOrDefault(c => c.CellIndex == config.CellIndex);
                if (cell == null) continue;
                if (cell.IsOccupied)
                    throw new InvalidOperationException($"Cell {config.CellIndex} already occupied");

                SpawnEnemy(config, cell);
                freeCells.Remove(cell);
            }
        }

        private void SpawnRandomCells(IReadOnlyList<EnemySpawnData> data, List<ICellGrid> freeCells)
        {
            for (int i = 0; i < data.Count; i++)
            {
                var config = data[i];
                if (config.CellIndex >= 0) continue;
                if (freeCells.Count == 0) break;

                var cell = freeCells[0];
                freeCells.RemoveAt(0);
                SpawnEnemy(config, cell);
            }
        }

        private void SpawnEnemy(EnemySpawnData config, ICellGrid cell)
        {
            var enemy = _unitBuilder
                .SetUnitType(UnitTypeId.Enemy)
                .SetUnitAttackType(config.UnitAttackTypeId)
                .SetCurrentLevel(config.LevelIndex)
                .SetPosition(cell.Transform.position)
                .Build();

            cell.SetUnit(enemy);
        }
    }
}