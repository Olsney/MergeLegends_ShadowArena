using Code.Infrastructure.Factory.Game;
using Code.Logic.EnemySpawners;
using Code.Logic.PlayArea;
using Code.Services.PersistentProgress;
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
        private readonly IPlayerUnitSpawner _playerUnitSpawner;
        private readonly BattleStateMachine _battleStateMachine;
        private readonly IPersistentProgressService _progressService;

        private UnitSpawnerViewsRoot _unitSpawnerViewsRoot;
        private MeleePlayerUnitSpawnerPresenter _meleeSpawnPresenter;
        private RangedPlayerUnitSpawnerPresenter _rangedSpawnPresenter;
        private FightPresenter _fightPresenter;
        private PlayZone _playZone;

        public BattlePlanningState(
            IUIFactory uiFactory,
            IGameFactory gameFactory,
            IEnemySpawner enemySpawner,
            ISpawnButtonsParentProvider spawnButtonsParentProvider,
            IPresenterFactory presenterFactory,
            IPlayerUnitSpawner playerUnitSpawner,
            BattleStateMachine battleStateMachine,
            IPersistentProgressService progressService)
        {
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _enemySpawner = enemySpawner;
            _spawnButtonsParentProvider = spawnButtonsParentProvider;
            _presenterFactory = presenterFactory;
            _playerUnitSpawner = playerUnitSpawner;
            _battleStateMachine = battleStateMachine;
            _progressService = progressService;
        }

        public void Enter()
        {
            _unitSpawnerViewsRoot = _uiFactory.CreateSpawnerViewsRoot();

            _meleeSpawnPresenter = _presenterFactory.CreateSpawnMeleePlayerUnitPresenter(
                _spawnButtonsParentProvider.TransformParent, _playerUnitSpawner);
            _rangedSpawnPresenter = _presenterFactory.CreateSpawnRangedPlayerUnitPresenter(
                _spawnButtonsParentProvider.TransformParent, _playerUnitSpawner);

            _fightPresenter = _presenterFactory.CreateFightPresenter(_battleStateMachine);
            _presenterFactory.CreateCurrencyPresenter(_progressService);
            _presenterFactory.CreateLevelPresenter(_progressService);

            _playZone = _gameFactory.CreatePlayZone();
            _enemySpawner.Spawn();
        }

        public void Exit()
        {
            _meleeSpawnPresenter.RemoveView();
            _rangedSpawnPresenter.RemoveView();
            _fightPresenter.RemoveView();

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