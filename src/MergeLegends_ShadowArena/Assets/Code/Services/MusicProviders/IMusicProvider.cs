using UnityEngine;
using UnityEngine.Audio;

namespace Code.Services.MusicProviders
{
    public interface IMusicProvider
    {
        AudioSource AudioSource { get; }
        AudioMixerGroup MusicMixerGroup { get; }
        void SetAudioSource(AudioSource audioSource);
        public void SetMusicMixerGroup(AudioMixerGroup musicMixerGroup);
    }
}