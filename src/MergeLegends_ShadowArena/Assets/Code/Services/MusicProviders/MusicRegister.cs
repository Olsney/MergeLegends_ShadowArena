using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Code.Services.MusicProviders
{
    public class MusicRegister : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _musicMixerGroup;
        
        private IMusicProvider _musicProvider;

        [Inject]
        private void Construct(IMusicProvider musicProvider) =>
            _musicProvider = musicProvider;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _musicProvider.SetAudioSource(GetComponent<AudioSource>());
            _musicProvider.SetMusicMixerGroup(_musicMixerGroup);
        }
    }
}