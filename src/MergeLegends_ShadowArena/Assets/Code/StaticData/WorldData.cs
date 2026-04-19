using System;

namespace Code.StaticData
{
    [Serializable]
    public class WorldData
    {
        public LevelData LevelData;
        public CurrencyData CurrencyData;

        public WorldData()
        {
            LevelData = new LevelData();
            CurrencyData = new CurrencyData();
        }
    }
}