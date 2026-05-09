using System;
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
using Zenject;

namespace Code.UI.Factory
{
    public class PresenterFactory : IPresenterFactory
    {
        private readonly IUIFactory _uiFactory;

        public PresenterFactory(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public UnitCanvasPresenter CreateUnitCanvasPresenter(UnitCanvasView view, UnitDeath unitDeath) 
            => CreatePresenter(view, canvasView => new UnitCanvasPresenter(canvasView, unitDeath));

        public UnitHealthBarPresenter CreateUnitHealthBarPresenter(UnitHealthBarView view, IHealth health)
            => CreatePresenter(view, healthBarView => new UnitHealthBarPresenter(healthBarView, health));

        public UnitLevelPresenter CreateUnitLevelPresenter(UnitLevelView view, IBattleService battleService,
            int unitCurrentLevelIndex)
            => CreatePresenter(view, levelView 
                => new UnitLevelPresenter(levelView, battleService, unitCurrentLevelIndex));

        public DamageFloatingPresenter CreateDamageFloatingPresenter(DamageFloatingView view, IHealth health)
            => CreatePresenter(view, floatingView => new DamageFloatingPresenter(floatingView, health));

        public MeleePlayerUnitPurchasePresenter CreateMeleePlayerUnitPurchasePresenter(
            Transform parent, IPlayerUnitPurchaseService purchaseService, IPersistentProgressService progressService)
        {
            MeleePlayerUnitPurchaseView purchaseView = _uiFactory.CreateMeleeSpawnButtonView(parent);
            return CreatePresenter(purchaseView, view 
                => new MeleePlayerUnitPurchasePresenter(view, purchaseService, progressService));
        }

        public RangedPlayerUnitPurchasePresenter CreateRangedPlayerUnitPurchasePresenter(
            Transform parent, IPlayerUnitPurchaseService purchaseService, IPersistentProgressService progressService)
        {
            RangedPlayerUnitPurchaseView purchaseView = _uiFactory.CreateRangedSpawnButtonView(parent);
            return CreatePresenter(purchaseView, view 
                => new RangedPlayerUnitPurchasePresenter(view, purchaseService, progressService));
        }

        public FightPresenter CreateFightPresenter(BattleStateMachine battleStateMachine)
        {
            StartFightView view = _uiFactory.CreateStartFightButtonView();
            return CreatePresenter(view, fightView => new FightPresenter(fightView, battleStateMachine));
        }

        public SettingsPresenter CreateSettingsPresenter()
        {
            OpenSettingsView view = _uiFactory.GetOpenSettingsView();
            return CreatePresenter(view, settingsView => new SettingsPresenter(settingsView, _uiFactory));
        }

        public CurrencyPresenter CreateCurrencyPresenter(IPersistentProgressService progressService)
        {
            CurrencyCounterView view = _uiFactory.GetCurrencyCounterView();
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