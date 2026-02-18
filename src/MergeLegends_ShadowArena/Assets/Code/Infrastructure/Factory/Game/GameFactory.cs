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

        public GameFactory(IInstantiator instantiator, IAssetProvider assets)
        {
            _instantiator = instantiator;
            _assets = assets;
        }

        public GameObject CreatePlayZone(Vector3 heroesGridZone, Vector3 enemiesGridZone)
        {
            _playZone = Instantiate(_assets.Load(AssetPath.PlayZone));

            CreateGridZone(heroesGridZone, AssetPath.HeroesGridZone);
            CreateGridZone(enemiesGridZone, AssetPath.EnemiesGridZone);

            return _playZone;
        }

        private void CreateGridZone(Vector3 position, string name)
        {
            var gridZone = Instantiate(_assets.Load(name));

            gridZone.transform.SetParent(_playZone.transform, true);
            gridZone.transform.localPosition = position;
            gridZone.name = name;

            GenerateGridCells(gridZone);
        }

        private void GenerateGridCells(GameObject gridZone)
        {
            var gridUnitPlaces = gridZone.GetComponent<GridUnitPlaces>();

            if (gridUnitPlaces == null)
                return;

            GameObject prefab = _assets.Load(AssetPath.CellGridZone);

            int cellsX = 5;
            int cellsZ = 3;
            float spacing = 1.85f;

            for (int x = 0; x < cellsX; x++)
            {
                for (int z = 0; z < cellsZ; z++)
                {
                    GameObject cell = Instantiate(prefab);

                    Vector3 localPosition = new Vector3(x * spacing, 0, z * spacing);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = localPosition;
                    cell.name = $"Cell {x} {z}";

                    gridUnitPlaces.AddCell(cell.GetComponent<CellGrid>());
                }
            }
        }

        public GameObject CreateHero(Vector3 position, Quaternion rotation)
            => Instantiate(_assets.Load(AssetPath.HeroLvl1), position, rotation);

        private GameObject Instantiate(GameObject prefab)
            => _instantiator.InstantiatePrefab(prefab);

        private GameObject Instantiate(GameObject prefab, Vector3 position)
            => _instantiator.InstantiatePrefab(prefab, position, Quaternion.identity, null);

        private GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
            => _instantiator.InstantiatePrefab(prefab, position, rotation, null);
    }
}