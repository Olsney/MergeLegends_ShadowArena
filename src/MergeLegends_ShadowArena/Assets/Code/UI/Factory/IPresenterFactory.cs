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
using Code.Units.Enemies;
using Code.Units.PlayerUnits;

namespace Code.UI.Factory
{
    public interface IPresenterFactory
    {
        FightPresenter CreateFightPresenter(BattleStateMachine battleStateMachine);
        SettingsPresenter CreateSettingsPresenter();
        CurrencyPresenter CreateCurrencyPresenter(IPersistentProgressService progressService);
        LevelPresenter CreateLevelPresenter(IPersistentProgressService progressService);
        WarningMessagePresenter CreateWarningMessagePresenter(string message);
        ReloadScenePresenter CreateReloadScenePresenter(ReloadSceneView view, IReloadSceneService reloadSceneService);
        VictoryPresenter CreateVictoryPresenter(VictoryView view, long rewardValue);
        LosePresenter CreateLosePresenter(LoseView view, long rewardValue);
        TeamHealthBarPresenter CreatePlayerTeamHealthBarPresenter(IPlayerUnitsProvider playerUnitsProvider);
        TeamHealthBarPresenter CreateEnemyTeamHealthBarPresenter(IEnemiesProvider enemiesProvider);
    }
}