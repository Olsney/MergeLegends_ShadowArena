using System;
using System.Collections;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Code.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IPlayerUnitSpawner _spawner;
        private readonly BattleStateMachine _battleStateMachine;

        private GameObject _uiRoot;

        public UIFactory(IInstantiator instantiator, IAssetProvider assets, IPlayerUnitSpawner spawner,
            BattleStateMachine battleStateMachine)
        {
            _instantiator = instantiator;
            _assets = assets;
            _spawner = spawner;
            _battleStateMachine = battleStateMachine;
        }

        public GameObject CreateUIRoot()
        {
            GameObject prefab = _assets.Load(AssetPath.UIRootPath);
            _uiRoot = _instantiator.InstantiatePrefab(prefab);

            return _uiRoot;
        }

        public GameObject CreateHud()
        {
            GameObject prefab = _assets.Load(AssetPath.HudPath);

            return _instantiator.InstantiatePrefab(prefab, _uiRoot.transform);
        }

        public PlayerSpawnerPresenter CreateSpawnPlayerUnitPresenter()
        {
            return CreatePresenter<PlayerUnitSpawnView, PlayerSpawnerPresenter>(
                AssetPath.UISpawnPlayerUnitButtonPath,
                (view) => new PlayerSpawnerPresenter(view, _spawner, _battleStateMachine)
            );
        }

        public FightPresenter CreateFightPresenter()
        {
            return CreatePresenter<StartFightView, FightPresenter>(
                AssetPath.UIStartFightButtonPath,
                (view) => new FightPresenter(view, _battleStateMachine)
            );
        }

        private TPresenter CreatePresenter<TView, TPresenter>(string assetPath, Func<TView, TPresenter> createPresenter)
            where TView : MonoBehaviour
            where TPresenter : IInitializable
        {
            var view = CreateView<TView>(assetPath);
            var presenter = createPresenter(view);
            presenter.Initialize();
            return presenter;
        }

        private TView CreateView<TView>(string assetPath) where TView : MonoBehaviour
        {
            GameObject prefab = _assets.Load(assetPath);
            GameObject view = _instantiator.InstantiatePrefab(prefab, _uiRoot.transform);
            return view.GetComponent<TView>();
        }
    }
}