using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Code.Services.SoundProviders
{
    public class SoundRegister : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;

        private ISoundProvider _soundProvider;

        [Inject]
        private void Construct(ISoundProvider soundProvider)
            => _soundProvider = soundProvider;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _soundProvider.SetAudioSource(GetComponent<AudioSource>());
            _soundProvider.SetAudioMixerGroup(_audioMixerGroup);
        }
    }
}