using Code.Infrastructure.Factory.Game;
using Code.Services.Audio;
using Code.Services.LevelServices;
using Code.Services.PersistentProgress;
using Code.Services.ReloadSceneService;
using Code.Services.RewardServices;
using Code.Services.SaveLoad;
using Code.StaticData;
using Code.UI.Factory;
using Code.UI.LossShowers;
using Code.UI.ReloadScenes;
using Code.UI.VictoryShowers;
using Cysharp.Threading.Tasks;

namespace Code.Logic.BattleResultShowers
{
    public class BattleResultService : IBattleResultService
    {
        private const int DealyToShowResultWindow = 800;

        private readonly IUIFactory _uiFactory;
        private readonly IPresenterFactory _presenterFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IRewardServices _rewardServices;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadProgress;
        private readonly IReloadSceneService _reloadSceneService;
        private readonly IAudioService _audioService;
        private readonly ILevelService _levelService;

        public BattleResultService(IUIFactory uiFactory, IGameFactory gameFactory, IRewardServices rewardServices,
            IPersistentProgressService progressService, ISaveLoadService saveLoadProgress,
            IPresenterFactory presenterFactory, IReloadSceneService reloadSceneService,
            IAudioService audioService, ILevelService levelService)
        {
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _rewardServices = rewardServices;
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
            _presenterFactory = presenterFactory;
            _reloadSceneService = reloadSceneService;
            _audioService = audioService;
            _levelService = levelService;
        }

        public async void StartVictoryProcess()
        {
            await StartVictoryProcessAsync();
        }

        public async void StartLossProcess()
        {
            await StartLoseProcessAsync();
        }

        private async UniTask StartVictoryProcessAsync()
        {
            int levelIndex = _progressService.Progress.WorldData.LevelData.CurrentLevelIndex;
            GameLevelData levelData = _levelService.GetLevelData(levelIndex);

            _rewardServices.TransferCoinsVictory();
            _progressService.Progress.WorldData.LevelData.UpLevel();
            _saveLoadProgress.SaveProgress();

            _gameFactory.CreateVictoryConfetti(levelData.Location);
            _audioService.PlaySound(GameSoundTypeId.GameWin);

            await UniTask.Delay(DealyToShowResultWindow);

            VictoryView victoryView = _uiFactory.CreateVictoryView();

            int rewardValue = levelData.VictoryRewardValue;
            var victoryPresenter = _presenterFactory.CreateVictoryPresenter(victoryView, rewardValue);
            _presenterFactory.CreateReloadScenePresenter(victoryView.GetComponentInChildren<ReloadSceneView>(),
                _reloadSceneService);

            await victoryPresenter.PlaySequenceAsync();
        }

        private async UniTask StartLoseProcessAsync()
        {
            _rewardServices.TransferCoinsVictory();
            _saveLoadProgress.SaveProgress();

            await UniTask.Delay(DealyToShowResultWindow);

            LoseView loseView = _uiFactory.CreateLoseView();

            _audioService.PlaySound(GameSoundTypeId.GameLose);
            
            int levelIndex = _progressService.Progress.WorldData.LevelData.CurrentLevelIndex;
            int rewardValue = _levelService.GetLevelData(levelIndex).LoseRewardValue;
            var losePresenter = _presenterFactory.CreateLosePresenter(loseView, rewardValue);
            _presenterFactory.CreateReloadScenePresenter(loseView.GetComponentInChildren<ReloadSceneView>(),
                _reloadSceneService);

            await losePresenter.PlaySequenceAsync();
        }
    }
}