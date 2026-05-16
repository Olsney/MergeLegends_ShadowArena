using System;
using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.Services.Windows;
using Code.StaticData;
using Code.UI.Currency;
using Code.UI.Fights;
using Code.UI.Levels;
using Code.UI.LossShowers;
using Code.UI.Rewards;
using Code.UI.Services.Windows;
using Code.UI.Settings;
using Code.UI.SpawnUnits;
using Code.UI.VictoryShowers;
using Code.UI.WarningMessages;
using Code.Units.UI.Health;
using Code.Units.UI.Levels;
using UnityEngine;
using Zenject;

namespace Code.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressWriters { get; } = new();

        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticDataService;

        private DiContainer _sceneContainer;
        private GameObject _uiRoot;
        private GameObject _topLayerCanvas;

        public UIFactory(
            IInstantiator instantiator,
            IAssetProvider assets,
            IStaticDataService staticDataService)
        {
            _instantiator = instantiator;
            _assets = assets;
            _staticDataService = staticDataService;
        }

        public void SetSceneContainer(DiContainer sceneContainer)
        {
            _sceneContainer = sceneContainer;
        }

        public GameObject CreateUIRoot()
        {
            GameObject prefab = _assets.Load(AssetPath.UIRootPath);
            _uiRoot = _sceneContainer.InstantiatePrefab(prefab);
            return _uiRoot;
        }

        public GameObject CreateTopLayerCanvas()
        {
            GameObject prefab = _assets.Load(AssetPath.TopLayerCanvasWindowPath);
            _topLayerCanvas = _sceneContainer.InstantiatePrefab(prefab);
            return _topLayerCanvas;
        }

        public GameObject CreateHud()
        {
            GameObject prefab = _assets.Load(AssetPath.HudPath);
            return _sceneContainer.InstantiatePrefab(prefab, _uiRoot.transform);
        }

        public UnitSpawnerViewsRoot CreateSpawnerViewsRoot()
        {
            GameObject prefab = _assets.Load(AssetPath.PurchaseButtonsRootPath);
            return _sceneContainer.InstantiatePrefab(prefab, _uiRoot.transform)
                .GetComponentInChildren<UnitSpawnerViewsRoot>();
        }

        public MeleePlayerUnitPurchaseView CreateMeleeSpawnButtonView(Transform parent)
            => CreateView<MeleePlayerUnitPurchaseView>(AssetPath.PurchaseMeleePlayerUnitButtonPath, parent);

        public RangedPlayerUnitPurchaseView CreateRangedSpawnButtonView(Transform parent)
            => CreateView<RangedPlayerUnitPurchaseView>(AssetPath.PurchaseRangedPlayerUnitButtonPath, parent);

        public StartFightView CreateStartFightButtonView()
            => CreateView<StartFightView>(AssetPath.UIStartFightButtonPath, _uiRoot.transform);

        public WarningMessageView CreateWarningMessageView()
            => CreateView<WarningMessageView>(AssetPath.WarningMessagePath, _uiRoot.transform);

        public OpenSettingsView GetOpenSettingsView()
            => FindExistingView<OpenSettingsView>();

        public CurrencyCounterView CreateCurrencyCounterView()
            => CreateView<CurrencyCounterView>(AssetPath.CurrencyWindowPath, _topLayerCanvas.transform);

        public LevelView GetLevelView()
            => FindExistingView<LevelView>();

        public UnitHealthBarView GetUnitHealthBarView()
            => FindExistingView<UnitHealthBarView>();

        public UnitLevelView GetUnitLevelView()
            => FindExistingView<UnitLevelView>();

        public VictoryView CreateVictoryView()
            => CreateView<VictoryView>(AssetPath.VictoryWindowPath, null);

        public LoseView CreateLoseView()
            => CreateView<LoseView>(AssetPath.LoseWindowPath, null);

        public WindowBase CreateWindow(WindowType windowType)
        {
            WindowData windowData = _staticDataService.GetWindow(windowType);
            var windowObject = _sceneContainer.InstantiatePrefab(windowData.Template, _uiRoot.transform);

            return windowType switch
            {
                WindowType.Settings => windowObject.GetComponent<SettingsWindow>(),
                _ => throw new NullReferenceException($"Unknown window type: {windowType}")
            };
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private TView FindExistingView<TView>() where TView : MonoBehaviour
        {
            TView view = _uiRoot.GetComponentInChildren<TView>(true);

            if (view == null)
                throw new NullReferenceException($"View {typeof(TView).Name} not found in UI root");

            return view;
        }

        private TView CreateView<TView>(string assetPath, Transform parent) where TView : MonoBehaviour
        {
            GameObject prefab = _assets.Load(assetPath);
            GameObject view = _sceneContainer.InstantiatePrefab(prefab, parent);

            RegisterProgressWatchers(view);

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