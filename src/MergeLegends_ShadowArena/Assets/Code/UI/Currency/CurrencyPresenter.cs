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
            OnCurrencyChanged();
            _progressService.Progress.WorldData.CurrencyData.Changed += OnCurrencyChanged;
        }

        public override void Dispose() 
            => _progressService.Progress.WorldData.CurrencyData.Changed -= OnCurrencyChanged;

        private void OnCurrencyChanged()
        {
            View.UpdateCounter(_progressService.Progress.WorldData.CurrencyData.Value);
        }

        public void UpdateValue() 
            => View.UpdateCounter(_progressService.Progress.WorldData.CurrencyData.Value);
    }
}