using System.Collections.Generic;
using System.Linq;
using Code.Services.Windows;
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
        private const string WindowStaticDataPath = "StaticData/UI/WindowStaticData";
        private const string WarningMessageStaticDataPath = "StaticData/UI/WarningMessageStaticData";

        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<UnitAttackTypeId, UnitStaticData> _playerUnits;
        private Dictionary<UnitAttackTypeId, UnitStaticData> _enemies;
        private Dictionary<GridPlacesTypeId, GridPlacesStaticData> _grids;
        private Dictionary<WindowType, WindowData> _windowConfigs;
        private Dictionary<WarningMessagesTypeId, WarningMessageData> _warningMessages;

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
                .ToDictionary(x => x.UnitAttackTypeId, x => x);

            _enemies = Resources
                .LoadAll<UnitStaticData>(EnemiesStaticDataPath)
                .ToDictionary(x => x.UnitAttackTypeId, x => x);

            _grids = Resources
                .LoadAll<GridPlacesStaticData>(GridsStaticDataPath)
                .ToDictionary(x => x.GridPlacesTypeId, x => x);

            _windowConfigs = Resources
                .Load<WindowStaticData>(WindowStaticDataPath)
                .WindowDatas
                .ToDictionary(x => x.windowType, x => x);

            _warningMessages = Resources
                .Load<WarningMessageStaticData>(WarningMessageStaticDataPath)
                .WarningMessages
                .ToDictionary(x => x.WarningMessagesTypeId, x => x);
        }

        public LevelStaticData GetLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
                ? staticData
                : null;

        public UnitStaticData GetPlayerUnit(UnitAttackTypeId unitAttackTypeId) =>
            _playerUnits.TryGetValue(unitAttackTypeId, out UnitStaticData staticData)
                ? staticData
                : null;

        public UnitStaticData GetEnemy(UnitAttackTypeId unitAttackTypeId) =>
            _enemies.TryGetValue(unitAttackTypeId, out UnitStaticData staticData)
                ? staticData
                : null;

        public GridPlacesStaticData GetGridPlaces(GridPlacesTypeId gridPlacesTypeId) =>
            _grids.TryGetValue(gridPlacesTypeId, out GridPlacesStaticData staticData)
                ? staticData
                : null;

        public WindowData GetWindow(WindowType windowType) =>
            _windowConfigs.TryGetValue(windowType, out WindowData windowData)
                ? windowData
                : null;

        public WarningMessageData GetWarningMessage(WarningMessagesTypeId warningTypeId) =>
            _warningMessages.TryGetValue(warningTypeId, out WarningMessageData warningData)
                ? warningData
                : null;
    }
}