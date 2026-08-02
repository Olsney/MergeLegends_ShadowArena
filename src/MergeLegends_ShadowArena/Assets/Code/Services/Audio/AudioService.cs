using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.Services.SoundProviders;
using Code.Services.StaticData;
using Code.StaticData;
using UnityEngine;

namespace Code.Services.Audio
{
    public class AudioService : IAudioService
    {
        private const string EffectVolume = "EffectVolume";
        private const float MinVolume = -80f;
        private const float MaxVolume = 0f;

        private readonly ISoundProvider _soundProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadProgress;

        public bool IsMuted { get; private set; }

        public AudioService(
            ISoundProvider soundProvider,
            IStaticDataService staticDataService,
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadProgress)
        {
            _soundProvider = soundProvider;
            _staticDataService = staticDataService;
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
        }

        public void PlaySound(GameSoundTypeId gameSoundTypeId)
        {
            AudioData soundData = _staticDataService.GetSoundData(gameSoundTypeId);

            if (soundData == null)
            {
                Debug.LogWarning($"Game sound '{gameSoundTypeId}' not found.");
                return;
            }

            _soundProvider.AudioSource.PlayOneShot(
                soundData.AudioClip,
                soundData.Volume);
        }

        public void SetMute(bool muted)
        {
            _progressService.Progress.WorldData.SettingsData.SoundSettingsData.IsMuted = muted;
            _saveLoadProgress.SaveProgress();

            IsMuted = muted;

            float volume = muted ? MinVolume : MaxVolume;
            _soundProvider.AudioMixerGroup.audioMixer.SetFloat(EffectVolume, volume);
        }
    }
}