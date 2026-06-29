using Code.Infrastructure.States;
using Code.Services.PersistentProgress;
using Code.Services.ReloadSceneService;
using Code.UI.Currency;
using Code.UI.Fights;
using Code.UI.Levels;
using Code.UI.LossShowers;
using Code.UI.ReloadScenes;
using Code.UI.Settings;
using Code.UI.VictoryShowers;
using Code.UI.WarningMessages;

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
        VictoryPresenter CreateVictoryPresenter(VictoryView view, int rewardValue);
        LosePresenter CreateLosePresenter(LoseView view, int rewardValue);
    }
}