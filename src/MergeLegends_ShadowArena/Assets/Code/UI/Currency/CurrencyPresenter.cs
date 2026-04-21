using Code.Services.PersistentProgress;

namespace Code.UI.Currency
{
    public class CurrencyPresenter : Presenter<CurrencyCounterView>
    {
        private readonly IPersistentProgressService _progressService;

        public CurrencyPresenter(CurrencyCounterView view, IPersistentProgressService progressService) : base(view)
        {
            _progressService = progressService;
        }

        public override void Initialize()
        {
            _progressService.Progress.WorldData.CurrencyData.Changed += OnCurrencyChanged;
            OnCurrencyChanged();
        }

        public override void Dispose() 
            => _progressService.Progress.WorldData.CurrencyData.Changed -= OnCurrencyChanged;

        private void OnCurrencyChanged()
        {
            int value = _progressService.Progress.WorldData.CurrencyData.Value;
            View.UpdateCounter(value);
        }
    }
}