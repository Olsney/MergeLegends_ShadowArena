using System.Linq;
using Code.Infrastructure.Factory.Game;
using Code.Infrastructure.Factory.Units;
using Code.Logic.PlayArea.Cell;
using Code.Services.StaticData;
using Code.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Logic.EnemySpawners
{
    public class EnemySpawner : IEnemySpawner
    {
        private const int FirstGameLevelIndex = 0;

        private readonly IUnitFactory _unitFactory;
        private readonly IEnemyCellsProvider _cellsProvider;
        private readonly IStaticDataService _staticDataService;

        public EnemySpawner(IUnitFactory unitFactory, IEnemyCellsProvider cellsProvider,
            IStaticDataService staticDataService)
        {
            _unitFactory = unitFactory;
            _cellsProvider = cellsProvider;
            _staticDataService = staticDataService;
        }

        public void Spawn()
        {
            var enemiesSpawnData =
                _staticDataService.GetLevel(SceneManager.GetActiveScene().name)
                    .SpawnEnemiesInGameLevels[FirstGameLevelIndex].EnemiesSpawnData;

            var freeCells = _cellsProvider.GetFreeCells().ToList();

            int enemiesToSpawn = Mathf.Min(enemiesSpawnData.Count, freeCells.Count);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                var config = enemiesSpawnData[i];
                var cell = freeCells[i];

                GameObject enemy = _unitFactory.CreateUnit(UnitTypeId.Enemy,
                    config.UnitAttackTypeId,
                    config.LevelIndex,
                    cell.Transform.position
                );

                cell.SetUnit(enemy);
            }
        }
    }
}