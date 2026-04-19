using System;
using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.States;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.Services.Windows;
using Code.StaticData;
using Code.UI.Fights;
using Code.UI.Services.Windows;
using Code.UI.Settings;
using Code.UI.SpawnUnits;
using UnityEngine;
using Zenject;

namespace Code.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IPlayerUnitSpawner _spawner;
        private readonly BattleStateMachine _battleStateMachine;
        private readonly IStaticDataService _staticDataService;

        private GameObject _uiRoot;

        public UIFactory(IInstantiator instantiator, IAssetProvider assets, IPlayerUnitSpawner spawner,
            BattleStateMachine battleStateMachine, IStaticDataService staticDataService)
        {
            _instantiator = instantiator;
            _assets = assets;
            _spawner = spawner;
            _battleStateMachine = battleStateMachine;
            _staticDataService = staticDataService;
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

        public UnitSpawnerViewsRoot CreateSpawnerViewsRoot()
        {
            GameObject prefab = _assets.Load(AssetPath.SpawnerButtonsRootPath);

            return _instantiator.InstantiatePrefab(prefab, _uiRoot.transform)
                .GetComponentInChildren<UnitSpawnerViewsRoot>();
        }

        public MeleePlayerUnitSpawnerPresenter CreateSpawnMeleePlayerUnitPresenter(Transform parent)
        {
            return CreatePresenter<SpawnPlayerUnitView, MeleePlayerUnitSpawnerPresenter>(
                AssetPath.UISpawnMeleePlayerUnitButtonPath,
                (view)
                    => new MeleePlayerUnitSpawnerPresenter(view, _spawner), parent);
        }

        public RangedPlayerUnitSpawnerPresenter CreateSpawnRangedPlayerUnitPresenter(Transform parent)
        {
            return CreatePresenter<SpawnPlayerUnitView, RangedPlayerUnitSpawnerPresenter>(
                AssetPath.UISpawnRangedPlayerUnitButtonPath,
                (view)
                    => new RangedPlayerUnitSpawnerPresenter(view, _spawner), parent);
        }

        public FightPresenter CreateFightPresenter()
        {
            return CreatePresenter<StartFightView, FightPresenter>(
                AssetPath.UIStartFightButtonPath,
                (view) => new FightPresenter(view, _battleStateMachine)
            );
        }

        public SettingsPresenter CreateSettingsPresenter()
        {
            return CreatePresenter<OpenSettingsView, SettingsPresenter>(
                AssetPath.UIOpenSettingsButtonPath,
                (view) => new SettingsPresenter(view, (SettingsWindow)CreateWindow(WindowType.Settings))
            );
        }

        private WindowBase CreateWindow(WindowType windowType)
        {
            WindowData windowData = _staticDataService.GetWindow(windowType);
            var windowObject = _instantiator.InstantiatePrefab(windowData.Template, _uiRoot.transform);

            switch (windowType)
            {
                case WindowType.Settings:
                    return windowObject.GetComponent<SettingsWindow>();
                default:
                    throw new NullReferenceException($"Unknown window type: {windowType}");
            }
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private TPresenter CreatePresenter<TView, TPresenter>(string assetPath, Func<TView, TPresenter> createPresenter,
            Transform parent = null)
            where TView : MonoBehaviour
            where TPresenter : IInitializable
        {
            var view = CreateView<TView>(assetPath, parent);
            var presenter = createPresenter(view);
            presenter.Initialize();
            return presenter;
        }

        private TView CreateView<TView>(string assetPath, Transform parent = null) where TView : MonoBehaviour
        {
            GameObject prefab = _assets.Load(assetPath);
            GameObject view = _instantiator.InstantiatePrefab(prefab, parent ?? _uiRoot.transform);
            return view.GetComponent<TView>();
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }
    }
}