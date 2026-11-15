using Code.StaticData;

namespace Code.Services.UnitStatsServices
{
    public interface IUnitStatsService
    {
        UnitLevelStatsData GetStats(UnitStaticData unitStaticData, int levelIndex);
    }
}