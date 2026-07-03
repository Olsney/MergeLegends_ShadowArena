using Code.Core.LoadingCurtains;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Builders;
using Code.Infrastructure.CameraProviders;
using Code.Infrastructure.Factory.Game;
using Code.Infrastructure.Factory.Projectile;
using Code.Infrastructure.Factory.State;
using Code.Infrastructure.Factory.Units;
using Code.Infrastructure.States;
using Code.Logic.BattleResultShowers;
using Code.Logic.BattleServices;
using Code.Logic.EnemySpawners;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.UnitInGridLogic;
using Code.Services.CameraMoves;
using Code.Services.CellsInitializerServices;
using Code.Services.Input;
using Code.Services.MergeUnits;
using Code.Services.PersistentProgress;
using Code.Services.PlayerUnitPurchases;
using Code.Services.Raycast;
using Code.Services.ReloadSceneService;
using Code.Services.RewardServices;
using Code.Services.SaveLoad;
using Code.Services.SceneProvider;
using Code.Services.StaticData;
using Code.UI.Currency;
using Code.UI.Factory;
using Code.UI.SpawnUnits;
using Code.Units.Enemies;
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
            BindInputService();
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
            Container.Bind<IProjectileFactory>().To<ProjectileFactory>().AsSingle();
            Container.Bind<IUnitFactory>().To<UnitFactory>().AsSingle();
            Container.Bind<IPresenterFactory>().To<PresenterFactory>().AsSingle();
            Container.Bind<IPurchasePresenterFactory>().To<PurchaseUnitPresenterFactory>().AsSingle();
            Container.Bind<IUnitPresenterFactory>().To<UnitPresenterFactory>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<RaycastService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerUnitGridSelectorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerUnitGridMoverService>().AsSingle();
            Container.Bind<IPlayerUnitCellsProvider>().To<PlayerUnitCellsProvider>().AsSingle();
            Container.Bind<IEnemiesProvider>().To<EnemiesProvider>().AsSingle();
            Container.Bind<IEnemyCellsProvider>().To<EnemyCellsProvider>().AsSingle();
            Container.Bind<IPlayerUnitsProvider>().To<PlayerUnitsProvider>().AsSingle();
            Container.Bind<IBattleService>().To<BattleService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
            Container.Bind<ISpawnButtonsParentProvider>().To<SpawnButtonsParentProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<MergeUnitsService>().AsSingle();
            Container.Bind<IUnitBuilder>().To<UnitBuilder>().AsSingle();
            Container.Bind<IPlayerUnitPurchaseService>().To<PlayerUnitPurchaseService>().AsSingle();
            Container.Bind<IBattleResultService>().To<BattleResultService>().AsSingle();
            Container.Bind<IRewardServices>().To<RewardServices>().AsSingle();
            Container.Bind<IReloadSceneService>().To<ReloadSceneService>().AsSingle();
            Container.Bind<ICurrencyPresenterProvider>().To<CurrencyPresenterProvider>().AsSingle();
            Container.Bind<ISceneProvider>().To<SceneProvider>().AsSingle();
            Container.Bind<ICellsInitializerService>().To<CellsInitializerService>().AsSingle();
            Container.Bind<ICameraMoverService>().To<CameraMoverService>().AsSingle();
            Container.Bind<ILoadingCurtainProvider>().To<LoadingCurtainProvider>().AsSingle();
            Container.Bind<ICinemachineProvider>().To<CinemachineProvider>().AsSingle();
            Container.Bind<ICameraShakeService>().To<CameraShakeService>().AsSingle();
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
            Container.Bind<IEnemySpawner>().To<EnemySpawner>().AsSingle();
        }
    }
}