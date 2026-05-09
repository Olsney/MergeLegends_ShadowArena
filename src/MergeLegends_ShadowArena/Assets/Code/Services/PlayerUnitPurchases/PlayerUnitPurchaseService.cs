using System;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Factory;
using Code.UI.SpawnUnits;

namespace Code.Services.PlayerUnitPurchases
{
    public class PlayerUnitPurchaseService : IPlayerUnitPurchaseService
    {
        private const string MainSceneName = "Main";
        private const string NoMoneyMessage = "No Money";

        private readonly IPlayerUnitSpawner _playerUnitSpawnerService;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IPresenterFactory _presenterFactory;

        public PlayerUnitPurchaseService(IPlayerUnitSpawner playerUnitSpawnerService,
            IPersistentProgressService progressService, IStaticDataService staticDataService,
            IPresenterFactory presenterFactory)
        {
            _playerUnitSpawnerService = playerUnitSpawnerService;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _presenterFactory = presenterFactory;
        }

        public void BuyUnit(UnitAttackTypeId attackType)
        {
            int price = GetPrice(attackType);

            if (HasEnoughCurrency(price) == false)
                return;

            SpendCurrency(price);
            IncreaseUnitPrice(attackType);
            SpawnUnit(attackType);
        }

        private int GetPrice(UnitAttackTypeId attackType)
        {
            return attackType switch
            {
                UnitAttackTypeId.Melee
                    => _progressService.Progress.WorldData.UnitsPriceData.meleeCurrentPrice.PriceValue,
                UnitAttackTypeId.Ranged
                    => _progressService.Progress.WorldData.UnitsPriceData.rangedCurrentPrice.PriceValue,
                _ => throw new ArgumentOutOfRangeException(nameof(attackType), attackType, null)
            };
        }

        private bool HasEnoughCurrency(int price)
        {
            if (_progressService.Progress.WorldData.CurrencyData.Value >= price)
                return true;

            _presenterFactory.CreateWarningMessagePresenter(NoMoneyMessage);

            return false;
        }

        private void SpendCurrency(int price) 
            => _progressService.Progress.WorldData.CurrencyData.Spend(price);

        private void IncreaseUnitPrice(UnitAttackTypeId attackType)
        {
            int increment = _staticDataService.GetGameStaticData(MainSceneName)
                .FirstGameLaunchData.UnitsStartingPrice.PriceIncrementPerPurchaseValue;

            switch (attackType)
            {
                case UnitAttackTypeId.Melee:
                    _progressService.Progress.WorldData.UnitsPriceData.meleeCurrentPrice.IncreasePrice(increment);
                    break;
                case UnitAttackTypeId.Ranged:
                    _progressService.Progress.WorldData.UnitsPriceData.rangedCurrentPrice.IncreasePrice(increment);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(attackType), attackType, null);
            }
        }

        private void SpawnUnit(UnitAttackTypeId attackType) 
            => _playerUnitSpawnerService.Spawn(attackType);
    }
}