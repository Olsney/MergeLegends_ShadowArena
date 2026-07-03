using Code.Infrastructure.Factory.Game;
using Code.Logic.EnemySpawners;
using Code.Logic.PlayArea;
using Code.Services.CameraMoves;
using Code.Services.CellsInitializerServices;
using Code.UI.Factory;
using Code.UI.Fights;
using Code.UI.SpawnUnits;

namespace Code.Infrastructure.States
{
    public class BattlePlanningState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IEnemySpawner _enemySpawner;
        private readonly ISpawnButtonsParentProvider _spawnButtonsParentProvider;
        private readonly IPresenterFactory _presenterFactory;
        private readonly IPurchasePresenterFactory _purchasePresenterFactory;
        private readonly BattleStateMachine _battleStateMachine;
        private readonly ICellsInitializerService _cellsInitializerService;
        private readonly ICameraMoverService _cameraMover;

        private UnitSpawnerViewsRoot _unitSpawnerViewsRoot;
        private MeleePlayerUnitPurchasePresenter _meleeSpawnPresenter;
        private RangedPlayerUnitPurchasePresenter _rangedSpawnPresenter;
        private FightPresenter _fightPresenter;
        private PlayZone _playZone;

        public BattlePlanningState(
            IUIFactory uiFactory,
            IGameFactory gameFactory,
            IEnemySpawner enemySpawner,
            ISpawnButtonsParentProvider spawnButtonsParentProvider,
            IPresenterFactory presenterFactory,
            BattleStateMachine battleStateMachine,
            IPurchasePresenterFactory purchasePresenterFactory,
            ICellsInitializerService cellsInitializerService,
            ICameraMoverService cameraMover)
        {
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _enemySpawner = enemySpawner;
            _spawnButtonsParentProvider = spawnButtonsParentProvider;
            _presenterFactory = presenterFactory;
            _battleStateMachine = battleStateMachine;
            _purchasePresenterFactory = purchasePresenterFactory;
            _cellsInitializerService = cellsInitializerService;
            _cameraMover = cameraMover;
        }

        public void Enter()
        {
            _unitSpawnerViewsRoot = _uiFactory.CreateSpawnerViewsRoot();

            _meleeSpawnPresenter = _purchasePresenterFactory.CreateMeleePlayerUnitPurchasePresenter(
                _spawnButtonsParentProvider.TransformParent);
            _rangedSpawnPresenter = _purchasePresenterFactory.CreateRangedPlayerUnitPurchasePresenter(
                _spawnButtonsParentProvider.TransformParent);

            _fightPresenter = _presenterFactory.CreateFightPresenter(_battleStateMachine);

            _playZone = _gameFactory.CreatePlayZone().GetComponent<PlayZone>();
            _cellsInitializerService.InitializeAllCells();
            _enemySpawner.Spawn();
            
            _cameraMover.StartMoveFirstTimeGameLater();
        }

        public void Exit()
        {
            _meleeSpawnPresenter.Dispose();
            _rangedSpawnPresenter.Dispose();
            _fightPresenter.Dispose();

            _meleeSpawnPresenter = null;
            _rangedSpawnPresenter = null;
            _fightPresenter = null;

            _unitSpawnerViewsRoot.Remove();
            _playZone.Remove();
        }
    }
}