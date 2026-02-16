using System.Collections.Generic;
using System.Linq;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Services.StaticData
{
  public class StaticDataService : IStaticDataService, IInitializable
  {
    private const string LevelsDataPath = "StaticData/Levels";
    
    private Dictionary<string, LevelStaticData> _levels;

    public void Initialize()
    {
      Load();
    }

    private void Load()
    {
      _levels = Resources
        .LoadAll<LevelStaticData>(LevelsDataPath)
        .ToDictionary(x => x.LevelKey, x => x);
    }

    public LevelStaticData ForLevel(string sceneKey) =>
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData
        : null;
  }
}