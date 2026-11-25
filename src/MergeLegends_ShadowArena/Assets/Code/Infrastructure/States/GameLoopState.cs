using Code.UI.Currency;
using Code.UI.Factory;
using Code.UI.TeamHealth;
using Code.Units.Enemies;
using Code.Units.PlayerUnits;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ICurrencyPresenterProvider _currencyPresenterProvider;
        private readonly IPresenterFactory _presenterFactory;
        private readonly IPlayerUnitsProvider _playerUnitsProvider;
        private readonly IEnemiesProvider _enemiesProvider;

        private TeamHealthBarPresenter _playerTeamHealthPresenter;
        private TeamHealthBarPresenter _enemyTeamHealthPresenter;

        public GameLoopState(ICurrencyPresenterProvider currencyPresenterProvider, IPresenterFactory presenterFactory,
            IPlayerUnitsProvider playerUnitsProvider, IEnemiesProvider enemiesProvider)
        {
            _currencyPresenterProvider = currencyPresenterProvider;
            _presenterFactory = presenterFactory;
            _playerUnitsProvider = playerUnitsProvider;
            _enemiesProvider = enemiesProvider;
        }

        public void Enter()
        {
            _playerTeamHealthPresenter = _presenterFactory.CreatePlayerTeamHealthBarPresenter(_playerUnitsProvider);
            _enemyTeamHealthPresenter = _presenterFactory.CreateEnemyTeamHealthBarPresenter(_enemiesProvider);
        }

        public void Exit()
        {
            _playerTeamHealthPresenter.Dispose();
            _enemyTeamHealthPresenter.Dispose();

            _playerTeamHealthPresenter = null;
            _enemyTeamHealthPresenter = null;

            _currencyPresenterProvider.Presenter.Dispose();
            _currencyPresenterProvider.ResetPresenter();
        }
    }
}