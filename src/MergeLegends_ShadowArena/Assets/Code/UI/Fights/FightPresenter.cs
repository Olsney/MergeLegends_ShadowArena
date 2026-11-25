using Code.Infrastructure.States;

namespace Code.UI.Fights
{
    public class FightPresenter : Presenter<StartFightView>
    {
        private readonly BattleStateMachine _battleStateMachine;

        public FightPresenter(StartFightView view, BattleStateMachine battleStateMachine) : base(view)
        {
            _battleStateMachine = battleStateMachine;
        }

        public override void Initialize()
            => View.FightClicked += _battleStateMachine.EnterFight;

        public override void Dispose()
        {
            View.FightClicked -= _battleStateMachine.EnterFight;
            View.Remove();
        }
    }
}