using Code.UI.Currency;
using Cysharp.Threading.Tasks;

namespace Code.UI.LossShowers
{
    public class LosePresenter : Presenter<LoseView>
    {
        private readonly ICurrencyPresenterProvider _currencyPresenterProvider;
        private readonly int _rewardValue;
        
        public LosePresenter(LoseView view, int rewardValue, 
            ICurrencyPresenterProvider currencyPresenterProvider) : base(view)
        {
            _rewardValue = rewardValue;
            _currencyPresenterProvider = currencyPresenterProvider;
        }

        public override void Initialize()
        {
            View.SetValueReward(_rewardValue);
        }

        public async UniTask PlaySequenceAsync()
        {
            await View.PlayContentAnimationAsync();
            _currencyPresenterProvider.Presenter.UpdateValue();
            await View.PlayRewardAnimationAsync();
            View.ShowReloadButton();
        }
    }
}