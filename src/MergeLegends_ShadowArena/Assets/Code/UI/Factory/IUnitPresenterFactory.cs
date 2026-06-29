using Code.Units;
using Code.Units.UI;
using Code.Units.UI.FloatingDamages;
using Code.Units.UI.Health;
using Code.Units.UI.Levels;

namespace Code.UI.Factory
{
    public interface IUnitPresenterFactory
    {
        UnitCanvasPresenter CreateUnitCanvasPresenter(UnitCanvasView view, UnitDeath unitDeath);
        UnitHealthBarPresenter CreateUnitHealthBarPresenter(UnitHealthBarView view, IHealth health);
        UnitLevelPresenter CreateUnitLevelPresenter(UnitLevelView view, int unitCurrentLevelIndex);
        DamageFloatingPresenter CreateDamageFloatingPresenter(DamageFloatingView view, IHealth health);
    }
}