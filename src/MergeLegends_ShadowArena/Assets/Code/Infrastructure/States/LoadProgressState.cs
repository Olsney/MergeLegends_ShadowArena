using System.Collections.Generic;
using Code.Data;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.Services.StaticData;
using Code.StaticData;

namespace Code.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string MainSceneName = "Main";
        private const int FirstLevelIndex = 0;

        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadProgress;
        private readonly IStaticDataService _staticDataService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadProgress, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>(MainSceneName);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress =
                _saveLoadProgress.LoadProgress()
                ?? InitNewProgress();
        }

        private PlayerProgress InitNewProgress()
        {
            var firstGameLaunchData = _staticDataService.GetGameStaticData(MainSceneName).FirstGameLaunchData;

            var progress = new PlayerProgress();

            progress.WorldData.LevelData.CurrentLevelIndex = FirstLevelIndex;
            progress.WorldData.CurrencyData.Value = firstGameLaunchData.CurrencyStarting.StartValue;

            progress.WorldData.UnitsPriceData.meleeCurrentPrice.PriceValue =
                firstGameLaunchData.UnitsStartingPrice.MeleePrice.StartValue;
            progress.WorldData.UnitsPriceData.rangedCurrentPrice.PriceValue =
                firstGameLaunchData.UnitsStartingPrice.RangedPrice.StartValue;

            progress.WorldData.CompositionTeamData.PlayerUnitInCells = new List<PlayerUnitStatsInCellData>();

            return progress;
        }
    }
}