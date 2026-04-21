using Code.Infrastructure.Factory.Game;
using Code.Logic.EnemySpawners;
using Code.Logic.PlayArea;
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

        private UnitSpawnerViewsRoot _unitSpawnerViewsRoot;
        private MeleePlayerUnitSpawnerPresenter _meleeSpawnPresenter;
        private RangedPlayerUnitSpawnerPresenter _rangedSpawnPresenter;
        private FightPresenter _fightPresenter;
        private PlayZone _playZone;

        public BattlePlanningState(IUIFactory uiFactory, IGameFactory gameFactory, IEnemySpawner enemySpawner,
            ISpawnButtonsParentProvider spawnButtonsParentProvider)
        {
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _enemySpawner = enemySpawner;
            _spawnButtonsParentProvider = spawnButtonsParentProvider;
        }

        public void Enter()
        {
            _unitSpawnerViewsRoot = _uiFactory.CreateSpawnerViewsRoot();

            _meleeSpawnPresenter =
                _uiFactory.CreateSpawnMeleePlayerUnitPresenter(_spawnButtonsParentProvider.TransformParent);
            _rangedSpawnPresenter =
                _uiFactory.CreateSpawnRangedPlayerUnitPresenter(_spawnButtonsParentProvider.TransformParent);

            _fightPresenter = _uiFactory.CreateFightPresenter();
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