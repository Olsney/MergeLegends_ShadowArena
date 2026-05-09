using Code.Infrastructure.States;
using Code.Logic.BattleServices;
using Code.Services.PersistentProgress;
using Code.Services.PlayerUnitPurchases;
using Code.UI.Currency;
using Code.UI.Fights;
using Code.UI.Levels;
using Code.UI.Settings;
using Code.UI.SpawnUnits;
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
        UnitLevelPresenter CreateUnitLevelPresenter(UnitLevelView view, IBattleService battleService,
            int unitCurrentLevelIndex);
        DamageFloatingPresenter CreateDamageFloatingPresenter(DamageFloatingView view, IHealth health);
        MeleePlayerUnitPurchasePresenter CreateMeleePlayerUnitPurchasePresenter(
            Transform parent, IPlayerUnitPurchaseService purchaseService, IPersistentProgressService progressService);
        RangedPlayerUnitPurchasePresenter CreateRangedPlayerUnitPurchasePresenter(
            Transform parent, IPlayerUnitPurchaseService purchaseService, IPersistentProgressService progressService);
        FightPresenter CreateFightPresenter(BattleStateMachine battleStateMachine);
        SettingsPresenter CreateSettingsPresenter();
        CurrencyPresenter CreateCurrencyPresenter(IPersistentProgressService progressService);
        LevelPresenter CreateLevelPresenter(IPersistentProgressService progressService);
        WarningMessagePresenter CreateWarningMessagePresenter(string message);
    }
}