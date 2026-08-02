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
        private const string GamesStaticDataPath = "StaticData/Games";
        private const string PlayerUnitsStaticDataPath = "StaticData/UnitsType/PlayerUnits";
        private const string EnemiesStaticDataPath = "StaticData/UnitsType/Enemies";
        private const string GridsStaticDataPath = "StaticData/GridPlaces";
        private const string WindowStaticDataPath = "StaticData/UI/WindowStaticData";
        private const string WarningMessageStaticDataPath = "StaticData/UI/WarningMessageStaticData";
        private const string CameraMovesDataPath = "StaticData/CameraMoves/CameraMoveData";
        private const string SoundsStaticDataPath = "StaticData/Audio/SoundStaticData";
        private const string MusicStaticDataPath = "StaticData/Audio/MusicStaticData";

        private Dictionary<string, GameStaticData> _gameStaticData;
        private Dictionary<UnitAttackTypeId, UnitStaticData> _playerUnits;
        private Dictionary<UnitAttackTypeId, UnitStaticData> _enemies;
        private Dictionary<UnitTypeId, GridPlacesStaticData> _grids;
        private Dictionary<WindowType, WindowData> _windowConfigs;
        private Dictionary<WarningMessagesTypeId, WarningMessageData> _warningMessages;
        private Dictionary<CameraMoveTypeId, CameraMoveData> _cameraMoves;
        private Dictionary<GameSoundTypeId, AudioData> _soundStaticData;
        private List<GameMusicData>  _gameMusicData;

        public void Initialize()
        {
            Load();
        }

        private void Load()
        {
            _gameStaticData = Resources
                .LoadAll<GameStaticData>(GamesStaticDataPath)
                .ToDictionary(x => x.SceneKey, x => x);

            _playerUnits = Resources
                .LoadAll<UnitStaticData>(PlayerUnitsStaticDataPath)
                .ToDictionary(x => x.UnitAttackTypeId, x => x);

            _enemies = Resources
                .LoadAll<UnitStaticData>(EnemiesStaticDataPath)
                .ToDictionary(x => x.UnitAttackTypeId, x => x);

            _grids = Resources
                .LoadAll<GridPlacesStaticData>(GridsStaticDataPath)
                .ToDictionary(x => x.UnitTypeId, x => x);

            _windowConfigs = Resources
                .Load<WindowStaticData>(WindowStaticDataPath)
                .WindowDatas
                .ToDictionary(x => x.windowType, x => x);

            _warningMessages = Resources
                .Load<WarningMessageStaticData>(WarningMessageStaticDataPath)
                .WarningMessages
                .ToDictionary(x => x.WarningMessagesTypeId, x => x);

            _cameraMoves = Resources
                .Load<CameraMoveStaticData>(CameraMovesDataPath)
                .CameraMoveDatas
                .ToDictionary(x => x.CameraMoveTypeId, x => x);

            _soundStaticData = Resources
                .Load<SoundStaticData>(SoundsStaticDataPath)
                .GameSoundDatas
                .ToDictionary(x => x.GameSoundTypeId, x => x.audioData);
            
            _gameMusicData = Resources
                .Load<MusicStaticData>(MusicStaticDataPath)
                .GameMusicDatas;
        }

        public GameStaticData GetGameStaticData(string sceneKey) =>
            _gameStaticData.TryGetValue(sceneKey, out GameStaticData staticData)
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

        public GridPlacesStaticData GetGridPlaces(UnitTypeId unitTypeId) =>
            _grids.TryGetValue(unitTypeId, out GridPlacesStaticData staticData)
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

        public CameraMoveData GetCameraMoveData(CameraMoveTypeId cameraMoveTypeId) =>
            _cameraMoves.TryGetValue(cameraMoveTypeId, out CameraMoveData cameraMoveData)
                ? cameraMoveData
                : null;

        public AudioData GetSoundData(GameSoundTypeId gameSoundTypeId) =>
            _soundStaticData.TryGetValue(gameSoundTypeId, out AudioData soundData)
                ? soundData
                : null;
        
        public List<GameMusicData> GetMusicData() =>
            _gameMusicData;
    }
}