using System;

namespace Code.StaticData
{
    [Serializable]
    public class UnitsPriceData
    {
        public MeleeCurrentPriceData meleeCurrentPrice;
        public RangedCurrentPriceData rangedCurrentPrice;

        public UnitsPriceData()
        {
            meleeCurrentPrice = new MeleeCurrentPriceData();
            rangedCurrentPrice = new RangedCurrentPriceData();
        }
    }
}