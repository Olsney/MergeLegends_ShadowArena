using Code.Infrastructure.Factory.Game;
using Code.UI.Factory;

namespace Code.Infrastructure.States
{
    public class BattlePlanningState : IState
    {
        private readonly BattleStateMachine _battleStateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;

        public BattlePlanningState(IUIFactory uiFactory, BattleStateMachine battleStateMachine, IGameFactory gameFactory)
        {
            _battleStateMachine = battleStateMachine;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _uiFactory.CreateSpawnPlayerUnitPresenter();
            _uiFactory.CreateFightPresenter();
            _battleStateMachine.InitPlayZone(_gameFactory.CreatePlayZone());
        }

        public void Exit()
        {
        }
    }
}