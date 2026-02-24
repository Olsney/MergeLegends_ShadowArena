using System;
using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;

        private GameObject _uiRoot;

        public event Action UiRootCreated;

        public UIFactory(IInstantiator instantiator, IAssetProvider assets)
        {
            _instantiator = instantiator;
            _assets = assets;
        }

        public GameObject CreateUIRoot()
        {
            GameObject prefab = _assets.Load(AssetPath.UIRoothPath);
            _uiRoot = _instantiator.InstantiatePrefab(prefab);
            UiRootCreated?.Invoke();

            return _uiRoot;
        }

        public GameObject CreateHud()
        {
            GameObject prefab = _assets.Load(AssetPath.HudPath);

            return _instantiator.InstantiatePrefab(prefab, _uiRoot.transform);
        }

        public GameObject CreateSpawnPlayerUnitView()
        {
            GameObject prefab = _assets.Load(AssetPath.UISpawnPlayerUnitButtonPath);

            return _instantiator.InstantiatePrefab(prefab, _uiRoot.transform);
        }
        
        public GameObject CreateStartFightView()
        {
            GameObject prefab = _assets.Load(AssetPath.UIStartFightButtonPath);

            return _instantiator.InstantiatePrefab(prefab, _uiRoot.transform);
        }
    }
}