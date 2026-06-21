using Code.Infrastructure.States;
using Code.Services.PersistentProgress;
using Code.Services.PlayerUnitPurchases;
using Code.Services.ReloadSceneService;
using Code.Services.RewardServices;
using Code.Services.StaticData;
using Code.UI.Currency;
using Code.UI.Fights;
using Code.UI.Levels;
using Code.UI.LossShowers;
using Code.UI.ReloadScenes;
using Code.UI.Rewards;
using Code.UI.Settings;
using Code.UI.SpawnUnits;
using Code.UI.VictoryShowers;
using Code.UI.WarningMessages;
using Code.Units;
using Code.Units.UI;
using Code.Units.UI.FloatingDamages;
using Code.Units.UI.Health;
using Code.Units.UI.Levels;
using UnityEngine;

namespace Code.UI.Factory
{
    public interface IPresenterFactory
    {
        UnitCanvasPresenter CreateUnitCanvasPresenter(UnitCanvasView view, UnitDeath unitDeath);
        UnitHealthBarPresenter CreateUnitHealthBarPresenter(UnitHealthBarView view, IHealth health);
        UnitLevelPresenter CreateUnitLevelPresenter(UnitLevelView view, IUnit unit,
            int unitCurrentLevelIndex);
        DamageFloatingPresenter CreateDamageFloatingPresenter(DamageFloatingView view, IHealth health);
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