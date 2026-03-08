using System.Linq;
using Code.Infrastructure.Factory.Game;
using Code.Logic.PlayArea.Cell;
using Code.Services.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : IEnemySpawner
{
    private readonly IGameFactory _gameFactory;
    private readonly IEnemyCellsProvider _cellsProvider;
    private readonly IStaticDataService _staticDataService;

    public EnemySpawner(IGameFactory gameFactory, IEnemyCellsProvider cellsProvider,
        IStaticDataService staticDataService)
    {
        _gameFactory = gameFactory;
        _cellsProvider = cellsProvider;
        _staticDataService = staticDataService;
    }

    public void Spawn()
    {
        var enemiesConfigs = 
            _staticDataService.GetLevel(SceneManager.GetActiveScene().name).EnemiesSpawnData;
    
        var freeCells = _cellsProvider.GetFreeCells().ToList();
    
        int enemiesToSpawn = Mathf.Min(enemiesConfigs.Count, freeCells.Count);
    
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            var config = enemiesConfigs[i];
            var cell = freeCells[i];
        
            GameObject enemy = _gameFactory.CreateEnemy(
                config.UnitTypeId, 
                config.UnitLvlTypeId, 
                cell.Transform.position
            );
        
            cell.SetUnit(enemy);
        }
    }
}