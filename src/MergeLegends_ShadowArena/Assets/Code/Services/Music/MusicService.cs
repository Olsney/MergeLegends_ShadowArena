using System;
using System.Collections.Generic;
using Code.Services.MusicProviders;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.Services.StaticData;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Services.Music
{
    public class MusicService : IMusicService
    {
        private const string MusicVolume = "MusicVolume";
        private const float MinVolume = -80f;
        private const float MaxVolume = 0f;
    
        private readonly IMusicProvider _musicProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadProgress;

        private readonly List<int> _playlist = new();

        private int _currentTrackIndex = -1;
        private bool _isActive;

        public bool IsMuted { get; private set; }

        public MusicService(
            IMusicProvider musicProvider,
            IStaticDataService staticDataService,
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadProgress)
        {
            _musicProvider = musicProvider;
            _staticDataService = staticDataService;
            _progressService = progressService;
            _saveLoadProgress = saveLoadProgress;
        }

        public void SetMute(bool muted)
        {
            _progressService.Progress.WorldData.SettingsData.MusicSettingsData.IsMuted = muted;
            _saveLoadProgress.SaveProgress();

            IsMuted = muted;

            float volume = muted ? MinVolume : MaxVolume;

            _musicProvider.MusicMixerGroup.audioMixer.SetFloat(
                MusicVolume,
                volume);
        }

        public void EnableMusic()
        {
            if (_isActive)
                return;

            _isActive = true;

            Shuffle();
            PlayNext();
        }

        public async UniTask PlayNext()
        {
            if (!_isActive)
                return;

            List<GameMusicData> music = _staticDataService.GetMusicData();

            if (music == null || music.Count == 0)
                return;

            _currentTrackIndex++;

            // закончился список - мешаем заново
            if (_currentTrackIndex >= _playlist.Count)
            {
                Shuffle();
                _currentTrackIndex = 0;
            }

            int trackId = _playlist[_currentTrackIndex];

            AudioData audioData = music[trackId].audioData;

            AudioSource source = _musicProvider.AudioSource;

            source.clip = audioData.AudioClip;
            source.volume = audioData.Volume;
            source.Play();

            await UniTask.Delay(
                TimeSpan.FromSeconds(audioData.AudioClip.length));

            if (_isActive)
                await PlayNext();
        }

        private void Shuffle()
        {
            Debug.Log("Shuffling");

            List<GameMusicData> music = _staticDataService.GetMusicData();

            if (music == null || music.Count == 0)
                return;

            _playlist.Clear();

            for (int i = 0; i < music.Count; i++)
                _playlist.Add(i);

            for (int i = _playlist.Count - 1; i > 0; i--)
            {
                int random = UnityEngine.Random.Range(0, i + 1);

                (_playlist[i], _playlist[random]) =
                    (_playlist[random], _playlist[i]);
            }
        }
    }
}