using Code.Logic.BattleServices;
using Code.UI;

namespace Code.Units.UI.Levels
{
    public class UnitLevelPresenter : Presenter<UnitLevelView>
    {
        private readonly IBattleService _battleService;
        private readonly int _unitLevelIndex;

        public UnitLevelPresenter(UnitLevelView view, IBattleService battleService, int unitLevelIndex) : base(view)
        {
            _battleService = battleService;
            _unitLevelIndex = unitLevelIndex;
        }

        public override void Initialize()
        {
            _battleService.FightStarting += View.Remove;
            View.SetValue(_unitLevelIndex);
        }

        public override void Dispose() 
            => _battleService.FightStarting -= View.Remove;
    }
}