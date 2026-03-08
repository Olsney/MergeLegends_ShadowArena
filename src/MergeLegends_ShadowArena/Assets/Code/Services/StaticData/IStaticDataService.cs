using Code.StaticData;

namespace Code.Services.StaticData
{
    public interface IStaticDataService
    {
        LevelStaticData GetLevel(string sceneKey);
        UnitStaticData GetPlayerUnit(UnitTypeId unitTypeId);
        UnitStaticData GetEnemy(UnitTypeId unitTypeId);
        GridPlacesStaticData GetGridPlaces(GridPlacesTypeId gridPlacesTypeId);
    }
}