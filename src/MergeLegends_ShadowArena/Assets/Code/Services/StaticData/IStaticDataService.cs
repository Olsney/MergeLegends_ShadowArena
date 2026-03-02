using Code.StaticData;

namespace Code.Services.StaticData
{
    public interface IStaticDataService
    {
        LevelStaticData GetLevel(string sceneKey);
        MeleeUnitStaticData GetMeleePlayerUnit(UnitLvlTypeId unitLvlTypeId);
        RangedUnitStaticData GetRangedPlayerUnit(UnitLvlTypeId unitLvlTypeId);
        GridPlacesStaticData GetGridPlaces(GridPlacesTypeId gridPlacesTypeId);
    }
}