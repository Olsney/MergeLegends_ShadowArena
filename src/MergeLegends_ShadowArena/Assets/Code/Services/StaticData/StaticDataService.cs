using System.Collections.Generic;
using System.Linq;
using Code.Logic.PlayArea.GridConfig;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Services.StaticData
{
    public class StaticDataService : IStaticDataService, IInitializable
    {
        private const string LevelStaticDataPath = "StaticData/Levels";
        private const string HeroesStaticDataPath = "StaticData/Heroes";
        private const string GridsStaticDataPath = "StaticData/GridPlaces";

        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<HeroTypeId, HeroStaticData> _heroes;
        private Dictionary<GridPlacesTypeId, GridPlacesStaticData> _grids;

        public void Initialize()
        {
            Load();
        }

        private void Load()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>(LevelStaticDataPath)
                .ToDictionary(x => x.LevelKey, x => x);
            
            _heroes = Resources
                .LoadAll<HeroStaticData>(HeroesStaticDataPath)
                .ToDictionary(x => x.HeroTypeId, x => x);
            
            _grids = Resources
                .LoadAll<GridPlacesStaticData>(GridsStaticDataPath)
                .ToDictionary(x => x.gridPlacesTypeId, x => x);
        }

        public LevelStaticData GetLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;

        public HeroStaticData GetHero(HeroTypeId heroTypeId) =>
            _heroes.TryGetValue(heroTypeId, out HeroStaticData staticData)
                ? staticData
                : null;
        
        public GridPlacesStaticData GetGridPlaces(GridPlacesTypeId gridPlacesTypeId) =>
            _grids.TryGetValue(gridPlacesTypeId, out GridPlacesStaticData staticData)
                ? staticData
                : null;
    }
}