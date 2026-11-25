using UnityEngine;
using UnityEngine.Audio;

namespace Code.Services.SoundProviders
{
    public class SoundProvider : ISoundProvider
    {
        public AudioSource AudioSource { get; private set; }
        
        public AudioMixerGroup AudioMixerGroup { get; private set; }

        public void SetAudioSource(AudioSource audioSource) 
            => AudioSource = audioSource;

        public void SetAudioMixerGroup(AudioMixerGroup audioMixerGroup) 
            => AudioMixerGroup = audioMixerGroup;
    }
}