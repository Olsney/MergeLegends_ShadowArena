using System;
using System.Collections.Generic;
using Code.Infrastructure.States;
using Code.Services.PersistentProgress;
using Code.Services.ReloadSceneService;
using Code.UI.Currency;
using Code.UI.Fights;
using Code.UI.Levels;
using Code.UI.LossShowers;
using Code.UI.ReloadScenes;
using Code.UI.Settings;
using Code.UI.TeamHealth;
using Code.UI.VictoryShowers;
using Code.UI.WarningMessages;
using Code.Units;
using Code.Units.Enemies;
using Code.Units.PlayerUnits;
using UnityEngine;
using Zenject;

namespace Code.UI.Factory
{
    public class PresenterFactory : IPresenterFactory
    {
        private readonly IUIFactory _uiFactory;
        private readonly ICurrencyPresenterProvider _currencyPresenterProvider;
        private readonly ISettingsComponentsPresenterFactory _settingsComponentsPresenterFactory;

        public PresenterFactory(IUIFactory uiFactory, ICurrencyPresenterProvider currencyPresenterProvider,
            ISettingsComponentsPresenterFactory settingsComponentsPresenterFactory)
        {
            _uiFactory = uiFactory;
            _currencyPresenterProvider = currencyPresenterProvider;
            _settingsComponentsPresenterFactory = settingsComponentsPresenterFactory;
        }

        public FightPresenter CreateFightPresenter(BattleStateMachine battleStateMachine)
        {
            StartFightView view = _uiFactory.CreateStartFightButtonView();
            return CreatePresenter(view, fightView => new FightPresenter(fightView, battleStateMachine));
        }

        public SettingsPresenter CreateSettingsPresenter()
        {
            OpenSettingsView view = _uiFactory.GetOpenSettingsView();
            return CreatePresenter(view, settingsView
                => new SettingsPresenter(settingsView, _uiFactory, _settingsComponentsPresenterFactory));
        }

        public CurrencyPresenter CreateCurrencyPresenter(IPersistentProgressService progressService)
        {
            CurrencyCounterView view = _uiFactory.CreateCurrencyCounterView();
            return CreatePresenter(view, currencyView => new CurrencyPresenter(currencyView, progressService));
        }

        public LevelPresenter CreateLevelPresenter(IPersistentProgressService progressService)
        {
            LevelView view = _uiFactory.GetLevelView();
            return CreatePresenter(view, levelView => new LevelPresenter(levelView, progressService));
        }

        public WarningMessagePresenter CreateWarningMessagePresenter(string message)
        {
            WarningMessageView view = _uiFactory.CreateWarningMessageView();
            return CreatePresenter(view, warningView => new WarningMessagePresenter(warningView, message));
        }

        public ReloadScenePresenter CreateReloadScenePresenter(ReloadSceneView view,
            IReloadSceneService reloadSceneService)
        {
            return CreatePresenter(view, reloadSceneView
                => new ReloadScenePresenter(reloadSceneView, reloadSceneService));
        }

        public VictoryPresenter CreateVictoryPresenter(VictoryView view, long rewardValue)
        {
            return CreatePresenter(view, loseView
                => new VictoryPresenter(loseView, rewardValue, _currencyPresenterProvider));
        }

        public LosePresenter CreateLosePresenter(LoseView view, long rewardValue)
        {
            return CreatePresenter(view, loseView
                => new LosePresenter(loseView, rewardValue, _currencyPresenterProvider));
        }

        public TeamHealthBarPresenter CreatePlayerTeamHealthBarPresenter(IPlayerUnitsProvider playerUnitsProvider)
            => CreateTeamHealthBarPresenter(
                _uiFactory.GetView<PlayerTeamHealthBarView>(),
                GetHealths(playerUnitsProvider.GetPlayerUnits()));

        public TeamHealthBarPresenter CreateEnemyTeamHealthBarPresenter(IEnemiesProvider enemiesProvider)
            => CreateTeamHealthBarPresenter(
                _uiFactory.GetView<EnemyTeamHealthBarView>(),
                GetHealths(enemiesProvider.GetEnemies()));

        private TeamHealthBarPresenter CreateTeamHealthBarPresenter(TeamHealthBarView view, List<IHealth> healths)
        {
            var presenter = new TeamHealthBarPresenter(view, healths);
            presenter.Initialize();
            return presenter;
        }

        private List<IHealth> GetHealths<TUnit>(IReadOnlyList<TUnit> units) where TUnit : MonoBehaviour
        {
            var healths = new List<IHealth>(units.Count);

            foreach (TUnit unit in units)
                if (unit != null && unit.TryGetComponent(out IHealth health))
                    healths.Add(health);

            return healths;
        }

        private TPresenter CreatePresenter<TView, TPresenter>(TView view, Func<TView, TPresenter> factory)
            where TView : MonoBehaviour
            where TPresenter : IInitializable
        {
            TPresenter presenter = factory(view);
            presenter.Initialize();
            return presenter;
        }
    }
}