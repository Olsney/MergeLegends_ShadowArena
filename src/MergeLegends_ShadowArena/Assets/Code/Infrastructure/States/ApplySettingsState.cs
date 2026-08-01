using Code.Services.Audio;
using Code.Services.Music;
using Code.Services.PersistentProgress;

namespace Code.Infrastructure.States
{
    public class ApplySettingsState : IState
    {
        private const string MainSceneName = "Main";

        private readonly GameStateMachine _gameStateMachine;
        private readonly IMusicService _musicService;
        private readonly IAudioService _audioService;
        private readonly IPersistentProgressService _persistentProgressService;

        public ApplySettingsState(GameStateMachine gameStateMachine, IMusicService musicService,
            IAudioService audioService, IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _musicService = musicService;
            _audioService = audioService;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter()
        {
            InitServices();

            _gameStateMachine.Enter<LoadLevelState, string>(MainSceneName);
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