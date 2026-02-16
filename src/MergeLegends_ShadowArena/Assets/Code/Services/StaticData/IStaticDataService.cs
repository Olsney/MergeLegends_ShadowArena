using Code.StaticData;

namespace Code.Services.StaticData
{
  public interface IStaticDataService
  {
    LevelStaticData ForLevel(string sceneKey);
  }
}