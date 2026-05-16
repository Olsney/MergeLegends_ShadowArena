using System;
using Code.UI;

namespace Code.Units.UI.Levels
{
    public class UnitLevelPresenter : Presenter<UnitLevelView>
    {
        private readonly int _unitLevelIndex;
        private readonly IUnit _unit;

        public UnitLevelPresenter(UnitLevelView view, IUnit unit, int unitLevelIndex) : base(view)
        {
            _unit = unit;
            _unitLevelIndex = unitLevelIndex;
        }

        public override void Initialize()
        {
            _unit.ProcessStarting += View.Remove;
            View.SetValue(_unitLevelIndex);
        }

        public override void Dispose()
            => _unit.ProcessStarting -= View.Remove;
    }
}