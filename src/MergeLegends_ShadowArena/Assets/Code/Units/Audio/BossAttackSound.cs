using UnityEngine;

namespace Code.Units.Audio
{
    public class BossAttackSound : MonoBehaviour
    {
        private AudioSource _audioSource;

        private AudioClip _attackClip;
        private float _attackVolume;

        private AudioClip _lastAttackClip;
        private float _lastAttackVolume;

        private void Awake() 
            => _audioSource = GetComponent<AudioSource>();

        public void Init(
            AudioClip attackClip,
            float attackVolume,
            AudioClip lastAttackClip,
            float lastAttackVolume)
        {
            _attackClip = attackClip;
            _attackVolume = attackVolume;

            _lastAttackClip = lastAttackClip;
            _lastAttackVolume = lastAttackVolume;
        }

        public void PlayAttackSound() 
            => Play(_attackClip, _attackVolume);

        public void PlayLastAttackSound() 
            => Play(_lastAttackClip, _lastAttackVolume);

        private void Play(AudioClip clip, float volume) 
            => _audioSource.PlayOneShot(clip, volume);
    }
}