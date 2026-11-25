using System;
using Code.Logic.BattleServices;
using Code.Units;
using Code.Units.UI;
using Code.Units.UI.FloatingDamages;
using Code.Units.UI.Health;
using Code.Units.UI.Levels;
using UnityEngine;
using Zenject;

namespace Code.UI.Factory
{
    public class UnitPresenterFactory : IUnitPresenterFactory
    {
        private readonly IBattleService _battleService;

        public UnitPresenterFactory(IBattleService battleService)
        {
            _battleService = battleService;
        }

        public UnitCanvasPresenter CreateUnitCanvasPresenter(UnitCanvasView view, UnitDeath unitDeath)
            => CreatePresenter(view, canvasView => new UnitCanvasPresenter(canvasView, unitDeath));

        public UnitHealthBarPresenter CreateUnitHealthBarPresenter(UnitHealthBarView view, IHealth health)
            => CreatePresenter(view, healthBarView => new UnitHealthBarPresenter(healthBarView, health));

        public UnitLevelPresenter CreateUnitLevelPresenter(UnitLevelView view, int unitCurrentLevelIndex)
            => CreatePresenter(view,
                levelView => new UnitLevelPresenter(levelView, _battleService, unitCurrentLevelIndex));

        public DamageFloatingPresenter CreateDamageFloatingPresenter(DamageFloatingView view, IHealth health)
            => CreatePresenter(view, floatingView => new DamageFloatingPresenter(floatingView, health));

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