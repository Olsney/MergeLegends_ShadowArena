using System;
using Code.Infrastructure.AssetManagement;
using Code.Logic.HeroSpawn;
using UnityEngine;
using Zenject;

namespace Code.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IHeroSpawnerService _heroSpawnerService;

        private GameObject _uiRoot;

        public UIFactory(IInstantiator instantiator, IAssetProvider assets, IHeroSpawnerService heroSpawnerService)
        {
            _instantiator = instantiator;
            _assets = assets;
            _heroSpawnerService = heroSpawnerService;
        }

        public GameObject CreateUIRoot()
        {
            GameObject prefab = _assets.Load(AssetPath.UIRoothPath);
            _uiRoot = _instantiator.InstantiatePrefab(prefab);

            return _uiRoot;
        }

        public GameObject CreateHud()
        {
            GameObject prefab = _assets.Load(AssetPath.HudPath);

            return _instantiator.InstantiatePrefab(prefab, _uiRoot.transform);
        }

        public GameObject CreateSpawnHeroView()
        {
            GameObject prefab = _assets.Load(AssetPath.SpawnHeroButton);

            GameObject view = _instantiator.InstantiatePrefab(prefab, _uiRoot.transform);
            view.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            
            _heroSpawnerService.InitializeView(view.GetComponent<IHeroSpawnView>());
            
            return view;
        }
    }
}