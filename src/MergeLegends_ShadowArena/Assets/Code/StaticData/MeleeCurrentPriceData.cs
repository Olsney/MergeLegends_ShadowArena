using System;

namespace Code.StaticData
{
    [Serializable]
    public class MeleeCurrentPriceData
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