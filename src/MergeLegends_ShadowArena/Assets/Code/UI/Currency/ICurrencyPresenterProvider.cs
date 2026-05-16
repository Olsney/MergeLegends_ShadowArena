namespace Code.UI.Currency
{
    public interface ICurrencyPresenterProvider
    {
        CurrencyPresenter Presenter { get; }
        void SetPresenter(CurrencyPresenter presenter);
    }
}