using Code.StaticData;

namespace Code.Services.LevelAutoGeneratorServices
{
    public interface ILevelAutoGeneratorService
    {
        GameLevelData GetGeneratedLevel(int levelIndex);
    }
}