using System;
using Code.Services.Audio;
using Code.Services.Music;
using Code.UI.Settings.AudioMute;
using Code.UI.Settings.MusicMute;
using UnityEngine;
using Zenject;

namespace Code.UI.Factory
{
    public class SettingsComponentsPresenterFactory : ISettingsComponentsPresenterFactory
    {
        private readonly IAudioService _audioService;
        private readonly IMusicService _musicService;

        public SettingsComponentsPresenterFactory(IAudioService audioService, IMusicService musicService)
        {
            _audioService = audioService;
            _musicService = musicService;
        }

        public AudioMutePresenter CreateAudioMutePresenter(AudioMuteView view) 
            => CreatePresenter(view, audioMuteView => new AudioMutePresenter(audioMuteView, _audioService));

        public MusicMutePresenter CreateMusicMutePresenter(MusicMuteView view) 
            => CreatePresenter(view, musicMuteView => new MusicMutePresenter(musicMuteView, _musicService));

        private TPresenter CreatePresenter<TView, TPresenter>(TView view, Func<TView, TPresenter> factory)
            where TView : MonoBehaviour
            where TPresenter : IInitializable
        {
            TPresenter presenter = factory(view);
            presenter.Initialize();
            return presenter;
        }
    }
}