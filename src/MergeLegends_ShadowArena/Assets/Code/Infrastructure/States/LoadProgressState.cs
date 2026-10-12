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

            _gameStateMachine.Enter<ApplySettingsState>();
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew()
        {
            PlayerProgress loadedProgress = _saveLoadProgress.LoadProgress();

            if (loadedProgress == null || loadedProgress.WorldData.TutorialData.IntroCompleted == false)
            {
                _progressService.Progress = InitNewProgress();
                return;
            }

            _progressService.Progress = loadedProgress;
        }

        private PlayerProgress InitNewProgress()
        {
            var firstGameLaunchData = _staticDataService.GetGameStaticData().FirstGameLaunchData;

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