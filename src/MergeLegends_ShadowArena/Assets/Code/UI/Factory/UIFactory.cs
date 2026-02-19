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

        public GameObject UiRoot { get; private set; }

        public event Action UIRootCreated;

        public UIFactory(IInstantiator instantiator, IAssetProvider assets)
        {
            _instantiator = instantiator;
            _assets = assets;
        }

        public GameObject CreateUIRoot()
        {
            GameObject prefab = _assets.Load(AssetPath.UIRoothPath);
            UiRoot = _instantiator.InstantiatePrefab(prefab);
            UIRootCreated?.Invoke();

            return UiRoot;
        }

        public GameObject CreateHud()
        {
            GameObject prefab = _assets.Load(AssetPath.HudPath);

            return _instantiator.InstantiatePrefab(prefab, UiRoot.transform);
        }

        public GameObject CreateSpawnHeroView()
        {
            GameObject prefab = _assets.Load(AssetPath.SpawnHeroButton);

            return _instantiator.InstantiatePrefab(prefab);
        }
    }
}