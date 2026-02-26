using Code.Infrastructure.States;

namespace Code.UI
{
    public class FightPresenter : Presenter<StartFightView>
    {
        private readonly BattleStateMachine _battleStateMachine;

        public FightPresenter(StartFightView view, BattleStateMachine battleStateMachine) : base(view)
        {
            _battleStateMachine = battleStateMachine;
        }

        public override void Initialize()
        {
            View.FightClicked += _battleStateMachine.EnterFight;
            _battleStateMachine.FightStarted += RemoveView;
        }

        public override void Dispose()
        {
            View.FightClicked -= _battleStateMachine.EnterFight;
            _battleStateMachine.FightStarted -= RemoveView;
        }

        private void RemoveView()
        {
            View.Remove();
        }
    }
}