using System;

namespace Code.StaticData
{
    [Serializable]
    public class CurrencyData
    {
        public long Value;
        public Action Changed;

        public void Spend(long amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Spend amount must be positive");

            Value -= amount;
            Changed?.Invoke();
        }

        public void Add(long amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Add amount must be positive");

            Value += amount;
        }
    }
}