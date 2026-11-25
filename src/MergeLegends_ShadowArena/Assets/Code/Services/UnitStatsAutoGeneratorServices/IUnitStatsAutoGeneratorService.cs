using Code.StaticData;

namespace Code.Services.UnitStatsAutoGeneratorServices
{
    public interface IUnitStatsAutoGeneratorService
    {
        UnitLevelStatsData GetGeneratedStats(UnitStaticData unitData, int levelIndex);
    }
}