using Code.UI.Currency;
using Cysharp.Threading.Tasks;

namespace Code.UI.VictoryShowers
{
    public class VictoryPresenter : Presenter<VictoryView>
    {
        private readonly ICurrencyPresenterProvider _currencyPresenterProvider;
        private readonly int _rewardValue;

        public VictoryPresenter(VictoryView view, int rewardValue,
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
            View.ShowNextLevelButton();
        }
    }
}