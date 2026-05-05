using Code.UI;

namespace Code.Units.UI
{
    public class UnitCanvasPresenter : Presenter<UnitCanvasView>
    {
        private readonly UnitDeath _unitDeath;
        
        public UnitCanvasPresenter(UnitCanvasView view, UnitDeath unitDeath) : base(view)
        {
            _unitDeath = unitDeath;
        }

        public override void Initialize() 
            => _unitDeath.Dead += View.Remove;

        public override void Dispose() 
            => _unitDeath.Dead -= View.Remove;
    }
}