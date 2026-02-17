using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory.Game
{
    public class GameFactory : IGameFactory
    {
        private const string HeroesGridZoneName = "HeroesGridZone";
        private const string EnemiesGridZoneName = "EnemiesGridZone";
        
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

            _heroesGridZone = CreateGridZone(heroesGridZone, HeroesGridZoneName);
            _enemiesGridZone = CreateGridZone(enemiesGridZone, EnemiesGridZoneName);

            return _playZone;
        }

        private GameObject CreateGridZone(Vector3 position, string name)
        {
            GameObject prefab = _assets.Load(AssetPath.UnitPlacesZone);
            GameObject gridZone = _instantiator.InstantiatePrefab(prefab);

            gridZone.transform.SetParent(_playZone.transform, true);
            gridZone.transform.localPosition = position;
            gridZone.transform.hasChanged = false;
            gridZone.name = name;

            GenerateGrid(gridZone.transform);

            return gridZone;
        }

        private void GenerateGrid(Transform gridZoneTransform)
        {
            GameObject prefab = _assets.Load(AssetPath.CellGridZone);
            float spacing = 2.5f;

            for (int x = 0; x < 5; x++)
            {
                for (int z = 0; z < 3; z++)
                {
                    Vector3 localPosition = new Vector3(x * spacing, 0, z * spacing);

                    GameObject spawnedTile = _instantiator.InstantiatePrefab(prefab);
                    spawnedTile.transform.SetParent(gridZoneTransform, true);
                    spawnedTile.transform.localPosition = localPosition;
                    spawnedTile.name = $"Tile {x} {z}";
                }
            }
        }
    }
}