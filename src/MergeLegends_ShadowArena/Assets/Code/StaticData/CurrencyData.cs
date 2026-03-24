using System;

namespace Code.StaticData
{
    [Serializable]
    public class CurrencyData
    {
        public int Value;
        public Action Changed;

        public void Collect(int value)
        {
            Value += value;
            Changed?.Invoke();
        }

        public void Add(int lootValue)
        {
            Value += lootValue;
            Changed?.Invoke();
        }
    }
}