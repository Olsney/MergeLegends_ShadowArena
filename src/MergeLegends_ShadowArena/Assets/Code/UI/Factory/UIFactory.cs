using System;
using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayerUnitSpawn;
using UnityEngine;
using Zenject;

namespace Code.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IPlayerSpawnerService playerSpawnerService;

        private GameObject _uiRoot;

        public UIFactory(IInstantiator instantiator, IAssetProvider assets, IPlayerSpawnerService playerSpawnerService)
        {
            _instantiator = instantiator;
            _assets = assets;
            this.playerSpawnerService = playerSpawnerService;
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

        public GameObject CreateSpawnPlayerUnitView()
        {
            GameObject prefab = _assets.Load(AssetPath.UISpawnPlayerUnitButtonPath);

            GameObject view = _instantiator.InstantiatePrefab(prefab, _uiRoot.transform);
            view.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            
            playerSpawnerService.InitializeView(view.GetComponent<IPlayerUnitSpawnView>());
            
            return view;
        }
    }
}