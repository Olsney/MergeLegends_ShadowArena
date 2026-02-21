using Code.StaticData;

namespace Code.Services.StaticData
{
  public interface IStaticDataService
  {
    LevelStaticData GetLevel(string sceneKey);
    HeroStaticData GetHero(HeroTypeId heroTypeId);
    GridPlacesStaticData GetGridPlaces(GridPlacesTypeId gridPlacesTypeId);
  }
}