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

        private PlayerSpawnerPresenter _spawnPresenter;
        private FightPresenter _fightPresenter;
        private GameObject _playZone;

        public BattlePlanningState(IUIFactory uiFactory, IGameFactory gameFactory)
        {
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _spawnPresenter = _uiFactory.CreateSpawnPlayerUnitPresenter();
            _fightPresenter = _uiFactory.CreateFightPresenter();
            _playZone = _gameFactory.CreatePlayZone();
        }

        public void Exit()
        {
            _spawnPresenter.RemoveView();
            _fightPresenter.RemoveView();

            _spawnPresenter?.Dispose();
            _fightPresenter?.Dispose();

            _spawnPresenter = null;
            _fightPresenter = null;

            GameObject.Destroy(_playZone);
        }
    }
}