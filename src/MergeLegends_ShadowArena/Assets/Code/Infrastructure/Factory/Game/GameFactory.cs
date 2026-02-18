using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayArea;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory.Game
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;

        private GameObject _playZone;
        private GameObject _heroesGridZone;
        private GameObject _enemiesGridZone;

        public GameFactory(IInstantiator instantiator, IAssetProvider assets)
        {
            _instantiator = instantiator;
            _assets = assets;
        }

        public GameObject CreatePlayZone(Vector3 heroesGridZone, Vector3 enemiesGridZone)
        {
            GameObject prefab = _assets.Load(AssetPath.PlayZone);
            _playZone = _instantiator.InstantiatePrefab(prefab);

            _heroesGridZone = CreateGridZone(heroesGridZone, AssetPath.HeroesGridZone);
            _enemiesGridZone = CreateGridZone(enemiesGridZone, AssetPath.EnemiesGridZone);

            return _playZone;
        }

        private GameObject CreateGridZone(Vector3 position, string name)
        {
            GameObject prefab = _assets.Load(name);
            GameObject gridZone = _instantiator.InstantiatePrefab(prefab);

            gridZone.transform.SetParent(_playZone.transform, true);
            gridZone.transform.localPosition = position;
            gridZone.transform.hasChanged = false;
            gridZone.name = name;

            GenerateGrid(gridZone);

            return gridZone;
        }

        private void GenerateGrid(GameObject gridZone)
        {
            GameObject prefab = _assets.Load(AssetPath.CellGridZone);
            float spacing = 2.5f;

            GridUnitPlaces gridUnitPlaces = null;
            List<CellGrid> cellGrids = new List<CellGrid>();

            if (gridZone.TryGetComponent<GridHeroesPlaces>(out GridHeroesPlaces gridHeroesPlaces))
                gridUnitPlaces = gridHeroesPlaces;

            if (gridZone.TryGetComponent<GridEnemiesPlaces>(out GridEnemiesPlaces gridEnemiesPlaces))
                gridUnitPlaces = gridEnemiesPlaces;

            for (int x = 0; x < 5; x++)
            {
                for (int z = 0; z < 3; z++)
                {
                    Vector3 localPosition = new Vector3(x * spacing, 0, z * spacing);

                    GameObject cell = _instantiator.InstantiatePrefab(prefab);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = localPosition;
                    cell.name = $"Cell {x} {z}";

                    cellGrids.Add(cell.GetComponent<CellGrid>());
                }
            }

            if (gridUnitPlaces != null)
                gridUnitPlaces.AddCell(cellGrids);
        }
    }
}