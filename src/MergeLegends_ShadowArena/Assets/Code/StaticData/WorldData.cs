using System;

namespace Code.StaticData
{
    [Serializable]
    public class WorldData
    {
        public LevelData LevelData;
        public CurrencyData CurrencyData;
        public UnitsPriceData UnitsPriceData;
        public CompositionTeamData CompositionTeamData;

        public WorldData()
        {
            LevelData = new LevelData();
            CurrencyData = new CurrencyData();
            UnitsPriceData = new UnitsPriceData();
            CompositionTeamData = new CompositionTeamData();
        }
    }
}