using System.Collections.Generic;
using System.Linq;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Services.StaticData
{
    public class StaticDataService : IStaticDataService, IInitializable
    {
        private const string LevelStaticDataPath = "StaticData/Levels";
        private const string PlayerUnitsStaticDataPath = "StaticData/UnitsType/PlayerUnits";
        private const string EnemiesStaticDataPath = "StaticData/UnitsType/Enemies";
        private const string GridsStaticDataPath = "StaticData/GridPlaces";

        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<UnitTypeId, UnitStaticData> _playerUnits;
        private Dictionary<UnitTypeId, UnitStaticData> _enemies;
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

            _playerUnits = Resources
                .LoadAll<UnitStaticData>(PlayerUnitsStaticDataPath)
                .ToDictionary(x => x.UnitTypeId, x => x);

            _enemies = Resources
                .LoadAll<UnitStaticData>(EnemiesStaticDataPath)
                .ToDictionary(x => x.UnitTypeId, x => x);

            _grids = Resources
                .LoadAll<GridPlacesStaticData>(GridsStaticDataPath)
                .ToDictionary(x => x.GridPlacesTypeId, x => x);
        }

        public LevelStaticData GetLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;

        public UnitStaticData GetPlayerUnit(UnitTypeId unitTypeId) =>
            _playerUnits.TryGetValue(unitTypeId, out UnitStaticData staticData)
                ? staticData
                : null;

        public UnitStaticData GetEnemy(UnitTypeId unitTypeId) =>
            _enemies.TryGetValue(unitTypeId, out UnitStaticData staticData)
                ? staticData
                : null;

        public GridPlacesStaticData GetGridPlaces(GridPlacesTypeId gridPlacesTypeId) =>
            _grids.TryGetValue(gridPlacesTypeId, out GridPlacesStaticData staticData)
                ? staticData
                : null;
    }
}