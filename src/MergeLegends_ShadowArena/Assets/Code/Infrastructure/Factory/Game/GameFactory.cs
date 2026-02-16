using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory.Game
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;

        public GameFactory(IInstantiator instantiator,
            IAssetProvider assets)
        {
            _instantiator = instantiator;
            _assets = assets;
        }

        public GameObject CreatePlayZone(Vector3 position)
        {
            GameObject prefab = _assets.Load(AssetPath.PlayZone);

            GameObject playZonePrefab = _instantiator.InstantiatePrefab(prefab);
            playZonePrefab.transform.position = position;
            
            return playZonePrefab;
        }
    }
}