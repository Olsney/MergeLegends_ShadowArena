using Code.Logic.Fight;
using Code.UI.Factory;

namespace Code.Infrastructure.States
{
    public class BattlePlanningState : IState
    {
        private readonly IUIFactory _uiFactory;
        private readonly BattleStateMachine _battleStateMachine;
        private IStartFightView _startFightView;

        public BattlePlanningState(
            IUIFactory uiFactory,
            BattleStateMachine battleStateMachine)
        {
            _uiFactory = uiFactory;
            _battleStateMachine = battleStateMachine;
        }

        public void Enter()
        {
            _startFightView = _uiFactory.CreateStartFightView().GetComponent<IStartFightView>();
            _startFightView.FightClicked += StartFight;
        }

        public void Exit()
        {
            _startFightView.FightClicked -= StartFight;
        }

        private void StartFight()
        {
            _battleStateMachine.EnterFight();
        }
    }
}