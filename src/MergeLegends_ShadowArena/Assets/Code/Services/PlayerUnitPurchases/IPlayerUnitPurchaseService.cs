using Code.StaticData;

namespace Code.Services.PlayerUnitPurchases
{
    public interface IPlayerUnitPurchaseService
    {
        void BuyUnit(UnitAttackTypeId attackType);
    }
}