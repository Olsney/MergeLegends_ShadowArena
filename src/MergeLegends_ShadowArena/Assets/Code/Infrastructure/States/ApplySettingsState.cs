using Code.Services.Audio;
using Code.Services.LevelServices;
using Code.Services.Music;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;
using Code.StaticData;

namespace Code.Infrastructure.States
{
    public class ApplySettingsState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IMusicService _musicService;
        private readonly IAudioService _audioService;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IStaticDataService _staticData;
        private readonly ILevelService _levelService;

        public ApplySettingsState(GameStateMachine gameStateMachine, IMusicService musicService,
            IAudioService audioService, IPersistentProgressService persistentProgressService,
            IStaticDataService staticData, ILevelService levelService)
        {
            _gameStateMachine = gameStateMachine;
            _musicService = musicService;
            _audioService = audioService;
            _persistentProgressService = persistentProgressService;
            _staticData = staticData;
            _levelService = levelService;
        }

        public void Enter()
        {
            InitServices();
            
            int levelIndex = _persistentProgressService.Progress.WorldData.LevelData.CurrentLevelIndex;
            LocationTypeId location = _levelService.GetLevelData(levelIndex).Location; 
            string sceneName = _staticData.GetLocation(location).SceneName;

            _gameStateMachine.Enter<LoadLevelState, string>(sceneName);
        }

        private void InitServices()
        {
            _musicService.SetMute(_persistentProgressService.Progress.WorldData.SettingsData.MusicSettingsData.IsMuted);
            _audioService.SetMute(_persistentProgressService.Progress.WorldData.SettingsData.SoundSettingsData.IsMuted);
            _musicService.EnableMusic();
        }

        public void Exit()
        {
        }
    }
}