using UnityEngine;
using UnityEngine.Audio;

namespace Code.Services.MusicProviders
{
    public class MusicProvider : IMusicProvider
    {
        public AudioSource AudioSource { get; private set; }
        public AudioMixerGroup MusicMixerGroup { get; private set; }

        public void SetAudioSource(AudioSource audioSource)
            => AudioSource = audioSource;

        public void SetMusicMixerGroup(AudioMixerGroup musicMixerGroup)
            => MusicMixerGroup = musicMixerGroup;
    }
}