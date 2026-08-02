using UnityEngine;
using UnityEngine.Audio;

namespace Code.Services.SoundProviders
{
    public interface ISoundProvider
    {
        AudioSource AudioSource { get; }
        AudioMixerGroup AudioMixerGroup { get; }
        void SetAudioSource(AudioSource audioSource);
        public void SetAudioMixerGroup(AudioMixerGroup audioMixerGroup);
    }
}