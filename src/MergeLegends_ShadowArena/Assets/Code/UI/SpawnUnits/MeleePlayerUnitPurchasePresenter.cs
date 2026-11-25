using Code.Services.PersistentProgress;
using Code.Services.PlayerUnitPurchases;
using Code.StaticData;

namespace Code.UI.SpawnUnits
{
    public class MeleePlayerUnitPurchasePresenter : Presenter<MeleePlayerUnitPurchaseView>
    {
        private readonly IPlayerUnitPurchaseService _purchaseService;
        private readonly IPersistentProgressService _progressService;

        public MeleePlayerUnitPurchasePresenter(MeleePlayerUnitPurchaseView view, IPlayerUnitPurchaseService purchase,
            IPersistentProgressService progressService) : base(view)
        {
            _purchaseService = purchase;
            _progressService = progressService;
        }

        public override void Initialize()
        {
            OnUpdatePrice();
            _progressService.Progress.WorldData.UnitsPriceData.meleeCurrentPrice.Changed += OnUpdatePrice;
            View.SpawnClicked += OnSpawnClicked;
        }

        public override void Dispose()
        {
            _progressService.Progress.WorldData.UnitsPriceData.meleeCurrentPrice.Changed -= OnUpdatePrice;
            View.SpawnClicked -= OnSpawnClicked;
            View.Remove();
        }

        private void OnSpawnClicked()
            => _purchaseService.BuyUnit(UnitAttackTypeId.Melee);
        
        private void OnUpdatePrice() 
            => View.ChangeValue(_progressService.Progress.WorldData.UnitsPriceData.meleeCurrentPrice.PriceValue);
    }
}