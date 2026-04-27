using Code.UI;

namespace Code.Units.UI
{
    public class UnitLevelPresenter : Presenter<UnitLevelView>
    {
        private readonly int _unitLevelIndex;
    
        public UnitLevelPresenter(UnitLevelView view, int unitLevelIndex) : base(view)
        {
            _unitLevelIndex = unitLevelIndex;
        }

        public override void Initialize() 
            => View.SetValue(_unitLevelIndex);
    }
}