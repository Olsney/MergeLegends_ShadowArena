namespace Code.UI.Currency
{
    public class CurrencyPresenterProvider : ICurrencyPresenterProvider
    {
        public CurrencyPresenter Presenter { get; private set; }

        public void SetPresenter(CurrencyPresenter presenter) 
            => Presenter = presenter;

        public void ResetPresenter() 
            => Presenter = null;
    }
}