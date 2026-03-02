using Code.Infrastructure.Factory.Game;
using Code.UI;
using Code.UI.Factory;
using UnityEngine;

namespace Code.Infrastructure.States
{
    public class BattlePlanningState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;

        private MeleePlayerUnitSpawnerPresenter _meleeSpawnPresenter;
        private RangedPlayerUnitSpawnerPresenter _rangedSpawnPresenter;
        private FightPresenter _fightPresenter;
        private GameObject _playZone;

        public BattlePlanningState(IUIFactory uiFactory, IGameFactory gameFactory)
        {
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _meleeSpawnPresenter = _uiFactory.CreateSpawnMeleePlayerUnitPresenter();
            _rangedSpawnPresenter = _uiFactory.CreateSpawnRangedPlayerUnitPresenter();
            _fightPresenter = _uiFactory.CreateFightPresenter();
            _playZone = _gameFactory.CreatePlayZone();
        }

        public void Exit()
        {
            _meleeSpawnPresenter.RemoveView();
            _rangedSpawnPresenter.RemoveView();
            _fightPresenter.RemoveView();

            _meleeSpawnPresenter?.Dispose();
            _rangedSpawnPresenter?.Dispose();
            _fightPresenter?.Dispose();

            _meleeSpawnPresenter = null;
            _rangedSpawnPresenter = null;
            _fightPresenter = null;

            GameObject.Destroy(_playZone);
        }
    }
}