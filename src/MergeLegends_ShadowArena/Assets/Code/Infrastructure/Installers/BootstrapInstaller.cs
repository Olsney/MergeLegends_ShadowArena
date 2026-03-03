using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.CameraProviders;
using Code.Infrastructure.Factory.Game;
using Code.Infrastructure.Factory.State;
using Code.Infrastructure.States;
using Code.Logic;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.UnitInGridLogic;
using Code.Services.Input;
using Code.Services.Raycast;
using Code.Services.StaticData;
using Code.UI;
using Code.UI.Factory;
using Code.UI.Services.Windows;
using Code.Units.PlayerUnit;
using Code.Units.PlayerUnits;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            BindCoroutine();
            BindFactories();
            BindStates();
            BindServices();
            BindSceneLoader();
            BindSpawners();
        }

        private void BindCoroutine() =>
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

        private void BindFactories()
        {
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<BattleStateMachine>().AsSingle();
            Container.Bind<BattlePlanningState>().AsSingle();
            Container.Bind<BattleFightState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }

        private void BindServices()
        {
            BindInputService();
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<RaycastService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerUnitGridSelectorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerUnitGridMoverService>().AsSingle();
            Container.Bind<ICellsProvider>().To<CellsProvider>().AsSingle();
            Container.Bind<IPlayerUnitProvider>().To<PlayerUnitProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<BattleService>().AsCached();
        }

        private void BindInputService()
        {
            if (Application.isEditor)
                Container.BindInterfacesAndSelfTo<StandaloneInputService>().AsSingle();
            else
                Container.BindInterfacesAndSelfTo<MobileInputService>().AsSingle();
        }

        private void BindSceneLoader() =>
            Container.Bind<SceneLoader>().AsSingle();

        private void BindSpawners()
        {
            Container.Bind<IPlayerUnitSpawner>().To<PlayerUnitSpawner>().AsSingle();
        }
    }
}