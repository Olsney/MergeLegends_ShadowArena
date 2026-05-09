using System;

namespace Code.StaticData
{
    [Serializable]
    public class CurrencyData
    {
        public int Value;
        public Action Changed;

        public void Spend(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Spend amount must be positive");

            Value -= amount;
            Changed?.Invoke();
        }

        public void Add(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Add amount must be positive");

            Value += amount;
            Changed?.Invoke();
        }
    }
}