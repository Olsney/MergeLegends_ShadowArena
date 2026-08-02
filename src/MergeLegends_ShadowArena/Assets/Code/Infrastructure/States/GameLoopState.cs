using Code.UI.Currency;

namespace Code.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ICurrencyPresenterProvider _currencyPresenterProvider;

        public GameLoopState(ICurrencyPresenterProvider currencyPresenterProvider)
        {
            _currencyPresenterProvider = currencyPresenterProvider;
        }

        public void Exit()
        {
            _currencyPresenterProvider.Presenter.Dispose();
            _currencyPresenterProvider.ResetPresenter();
        }

        public void Enter()
        {
        }
    }
}