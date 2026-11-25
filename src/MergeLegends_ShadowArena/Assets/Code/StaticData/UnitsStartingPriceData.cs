using System;

namespace Code.StaticData
{
    [Serializable]
    public class UnitsStartingPriceData
    {
        public int PriceIncrementPerPurchaseValue;
        public MeleeStartingPriceData MeleePrice;
        public RangedStartingPriceData RangedPrice;
    }
}