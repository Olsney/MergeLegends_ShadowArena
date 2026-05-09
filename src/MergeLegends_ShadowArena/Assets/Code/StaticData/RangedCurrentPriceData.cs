using System;

namespace Code.StaticData
{
    [Serializable]
    public class RangedCurrentPriceData
    {
        public int PriceValue;

        public Action Changed;

        public void IncreasePrice(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Increase amount must be positive");

            PriceValue += amount;
            Changed?.Invoke();
        }
    }
}