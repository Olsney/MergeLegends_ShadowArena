using Code.StaticData;

namespace Code.Services.LevelServices
{
    public interface ILevelService
    {
        GameLevelData GetLevelData(int levelNumber);
    }
}