using System;

namespace Code.StaticData
{
    [Serializable]
    public class SettingsData
    {
        public MusicSettingsData MusicSettingsData;
        public SoundSettingsData SoundSettingsData;

        public SettingsData()
        {
            MusicSettingsData = new MusicSettingsData();
            SoundSettingsData = new SoundSettingsData();
        }
    }
}