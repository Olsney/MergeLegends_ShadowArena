using System.Linq;
using Code.Infrastructure.Builders;
using Code.Logic.PlayArea.Cell;
using Code.Services.StaticData;
using Code.StaticData;
using UnityEngine;

namespace Code.Logic.EnemySpawners
{
    public class EnemySpawner : IEnemySpawner
    {
        private const int FirstGameLevelIndex = 0;
        private const string MainSceneName = "Main";

        private readonly IUnitBuilder _unitBuilder;
        private readonly IEnemyCellsProvider _cellsProvider;
        private readonly IStaticDataService _staticDataService;

        public EnemySpawner(
            IUnitBuilder unitBuilder,
            IEnemyCellsProvider cellsProvider,
            IStaticDataService staticDataService)
        {
            _unitBuilder = unitBuilder;
            _cellsProvider = cellsProvider;
            _staticDataService = staticDataService;
        }

        public void Spawn()
        {
            var enemiesSpawnData =
                _staticDataService.GetGameStaticData(MainSceneName)
                    .SpawnEnemiesInGameLevels[FirstGameLevelIndex].EnemiesSpawnData;

            var freeCells = _cellsProvider.GetFreeCells().ToList();

            int enemiesToSpawn = Mathf.Min(enemiesSpawnData.Count, freeCells.Count);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                var config = enemiesSpawnData[i];
                var cell = freeCells[i];

                GameObject enemy = _unitBuilder
                    .SetUnitType(UnitTypeId.Enemy)
                    .SetUnitAttackType(config.UnitAttackTypeId)
                    .SetCurrentLevel(config.LevelIndex)
                    .SetPosition(cell.Transform.position)
                    .Build();

                cell.SetUnit(enemy);
            }
        }
    }
}