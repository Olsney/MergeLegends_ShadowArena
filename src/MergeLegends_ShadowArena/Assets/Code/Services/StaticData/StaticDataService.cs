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
        private const string MeleePlayerUnitsStaticDataPath = "StaticData/UnitsType/PlayerUnit/Melee";
        private const string RangedPlayerUnitsStaticDataPath = "StaticData/UnitsType/PlayerUnit/Ranged";
        private const string GridsStaticDataPath = "StaticData/GridPlaces";

        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<UnitLvlTypeId, MeleeUnitStaticData> _meleePlayerUnits;
        private Dictionary<UnitLvlTypeId, RangedUnitStaticData> _rangedPlayerUnits;
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

            _meleePlayerUnits = Resources
                .LoadAll<MeleeUnitStaticData>(MeleePlayerUnitsStaticDataPath)
                .ToDictionary(x => x.UnitLvlTypeId, x => x);
            
            _rangedPlayerUnits = Resources
                .LoadAll<RangedUnitStaticData>(RangedPlayerUnitsStaticDataPath)
                .ToDictionary(x => x.UnitLvlTypeId, x => x);

            _grids = Resources
                .LoadAll<GridPlacesStaticData>(GridsStaticDataPath)
                .ToDictionary(x => x.gridPlacesTypeId, x => x);
        }

        public LevelStaticData GetLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;

        public MeleeUnitStaticData GetMeleePlayerUnit(UnitLvlTypeId unitLvlTypeId) =>
            _meleePlayerUnits.TryGetValue(unitLvlTypeId, out MeleeUnitStaticData staticData)
                ? staticData
                : null;
        
        public RangedUnitStaticData GetRangedPlayerUnit(UnitLvlTypeId unitLvlTypeId) =>
            _rangedPlayerUnits.TryGetValue(unitLvlTypeId, out RangedUnitStaticData staticData)
                ? staticData
                : null;

        public GridPlacesStaticData GetGridPlaces(GridPlacesTypeId gridPlacesTypeId) =>
            _grids.TryGetValue(gridPlacesTypeId, out GridPlacesStaticData staticData)
                ? staticData
                : null;
    }
}