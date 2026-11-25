using System;
using Code.StaticData.Tutorial;

namespace Code.StaticData
{
    [Serializable]
    public class WorldData
    {
        public LevelData LevelData;
        public CurrencyData CurrencyData;
        public UnitsPriceData UnitsPriceData;
        public CompositionTeamData CompositionTeamData;
        public SettingsData SettingsData;
        public TutorialData TutorialData;

        public WorldData()
        {
            LevelData = new LevelData();
            CurrencyData = new CurrencyData();
            UnitsPriceData = new UnitsPriceData();
            CompositionTeamData = new CompositionTeamData();
            SettingsData = new SettingsData();
            TutorialData = new TutorialData();
        }
    }
}