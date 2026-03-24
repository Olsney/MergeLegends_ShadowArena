using System;
using Code.StaticData;

namespace Code.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        
        public PlayerProgress()
        {
            WorldData = new WorldData();
        }
    }
}