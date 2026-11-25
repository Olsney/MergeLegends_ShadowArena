using UnityEngine;

namespace Code.Units.Audio
{
    public class BossLastAttackSound : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Init(AudioClip audioClip, float volume)
        {
            _audioSource.clip = audioClip;
            _audioSource.volume = volume;
        }

        public void PlayHitSound(bool pitchActive = false)
        {
            StopSound();
            
            if (pitchActive)
                _audioSource.pitch = Random.Range(0.90f, 1.1f);

            _audioSource.PlayOneShot(_audioSource.clip, _audioSource.volume);
        }

        public void StopSound()
        {
            _audioSource.Stop();
        }
    }
}
