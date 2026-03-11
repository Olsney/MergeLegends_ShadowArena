using Code.StaticData;

namespace Code.Services.StaticData
{
    public interface IStaticDataService
    {
        LevelStaticData GetLevel(string sceneKey);
        UnitStaticData GetPlayerUnit(UnitAttackTypeId unitAttackTypeId);
        UnitStaticData GetEnemy(UnitAttackTypeId unitAttackTypeId);
        GridPlacesStaticData GetGridPlaces(GridPlacesTypeId gridPlacesTypeId);
    }
}