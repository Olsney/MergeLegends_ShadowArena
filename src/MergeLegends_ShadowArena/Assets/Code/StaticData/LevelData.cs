using System;

namespace Code.StaticData
{
    [Serializable]
    public class LevelData
    {
        public int CurrentLevelIndex;

        public void UpLevel()
        {
            CurrentLevelIndex++;
        }
    }
}