using UnityEngine;

namespace Code.Units.Audio
{
    public class MeleeAttackSound : MonoBehaviour
    {
        private const float MaxInclusive = 1.1f;
        private const float MinInclusive = 0.90f;

        private AudioSource _audioSource;

        private void Awake()
            => _audioSource = GetComponent<AudioSource>();

        public void Init(AudioClip audioClip, float volume)
        {
            _audioSource.clip = audioClip;
            _audioSource.volume = volume;
        }

        public void PlayHitSound(bool pitchActive = false)
        {
            _audioSource.Stop();

            if (pitchActive)
                _audioSource.pitch = Random.Range(MinInclusive, MaxInclusive);

            _audioSource.PlayOneShot(_audioSource.clip, _audioSource.volume);
        }
    }
}