using Code.Logic.HideViewService;

namespace Code.Infrastructure.States
{
    public class BattleFightState : IState
    {
        //private readonly IBattleService _battleService;
        private readonly IHideViewService _hideViewService;

        public BattleFightState(
            //IBattleService battleService,
            IHideViewService hideViewService)
        {
            //_battleService = battleService;
            _hideViewService = hideViewService;
        }

        public void Enter()
        {
            _hideViewService.HideAll();
            //_battleService.StartBattle();
        }

        public void Exit()
        {
            //_battleService.StopBattle();
        }
    }
}