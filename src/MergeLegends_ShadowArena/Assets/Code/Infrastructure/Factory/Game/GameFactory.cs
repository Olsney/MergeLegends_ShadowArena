using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.Grid;
using Code.Services.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.Factory.Game
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticDataService;

        private GameObject _heroLvl1Prefab;
        private GameObject _playZone;

        public GameFactory(IInstantiator instantiator, IAssetProvider assets, IStaticDataService staticDataService)
        {
            _instantiator = instantiator;
            _assets = assets;
            _staticDataService = staticDataService;
            _heroLvl1Prefab = _assets.Load(AssetPath.HeroLvl1);
        }

        public GameObject CreatePlayZone(Vector3 heroesGridZone, Vector3 enemiesGridZone)
        {
            _playZone = _instantiator.InstantiatePrefab(_assets.Load(AssetPath.PlayZone));

            CreateGridZone(heroesGridZone, AssetPath.HeroesGridZone);
            CreateGridZone(enemiesGridZone, AssetPath.EnemiesGridZone);

            return _playZone;
        }

        private void CreateGridZone(Vector3 position, string name)
        {
            var gridZone = _instantiator.InstantiatePrefab(_assets.Load(name));

            gridZone.transform.SetParent(_playZone.transform, true);
            gridZone.transform.localPosition = position;
            gridZone.name = name;

            GenerateGridCells(gridZone, name);
        }

        private void GenerateGridCells(GameObject gridZone, string name)
        {
            if (name == AssetPath.HeroesGridZone)
            {
                var gridHeroesPlaces = gridZone.GetComponent<GridHeroesPlaces>();

                if (gridHeroesPlaces == null)
                    return;

                GenerateCellsForGrid(gridZone, gridHeroesPlaces, name);
            }

            if (name == AssetPath.EnemiesGridZone)
            {
                var gridEnemiesPlaces = gridZone.GetComponent<GridEnemiesPlaces>();

                if (gridEnemiesPlaces == null)
                    return;

                GenerateCellsForGrid(gridZone, gridEnemiesPlaces, name);
            }
        }

        private void GenerateCellsForGrid(GameObject gridZone, IGridUnitPlaces gridUnitPlaces, string name)
        {
            var levelStaticData = _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
            
            int width = levelStaticData.GridConfig.Width;
            int height = levelStaticData.GridConfig.Height;
            float spacing = levelStaticData.GridConfig.Spacing;

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject prefab = name == AssetPath.HeroesGridZone
                        ? _assets.Load(AssetPath.CellGridZoneHero)
                        : _assets.Load(AssetPath.CellGridZoneEnemy);

                    GameObject cell = _instantiator.InstantiatePrefab(prefab);

                    Vector3 localPosition = new Vector3(x * spacing, 0, z * spacing);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = localPosition;
                    cell.name = $"Cell {x} {z}";

                    gridUnitPlaces.AddCell(cell.GetComponent<ICellGrid>());
                }
            }
        }

        public GameObject CreateHero(Quaternion rotation)
            => _instantiator.InstantiatePrefab(_heroLvl1Prefab, Vector3.zero, rotation, null);
    }
}