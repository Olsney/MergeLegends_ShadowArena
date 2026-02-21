using Code.StaticData;

namespace Code.Services.StaticData
{
  public interface IStaticDataService
  {
    LevelStaticData GetLevel(string sceneKey);
    PlayerUnitStaticData GetPlayerUnit(PlayerUnitTypeId playerUnitTypeId);
    GridPlacesStaticData GetGridPlaces(GridPlacesTypeId gridPlacesTypeId);
  }
}