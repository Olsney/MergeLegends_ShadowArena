using UnityEngine;

namespace Code.Units.Audio
{
    public class RangedAttackSound : MonoBehaviour
    {
        private AudioSource _audioSource;

        private AudioClip _spawnClip;
        private AudioClip _explodeClip;

        private float _spawnVolume;
        private float _explodeVolume;

        private void Awake() 
            => _audioSource = GetComponent<AudioSource>();

        public void Init(AudioClip spawnClip, float spawnVolume, AudioClip explodeClip, float explodeVolume)
        {
            _spawnClip = spawnClip;
            _explodeClip = explodeClip;

            _spawnVolume = spawnVolume;
            _explodeVolume = explodeVolume;
        }

        public void PlaySpawnMagicSphereSound() 
            => Play(_spawnClip, _spawnVolume);

        public void PlayExplosionMagicSphereSound() 
            => Play(_explodeClip, _explodeVolume);

        private void Play(AudioClip clip, float volume)
        {
            _audioSource.Stop();

            _audioSource.clip = clip;
            _audioSource.volume = volume;
            _audioSource.pitch = Random.Range(0.95f, 1.05f);

            _audioSource.Play();
        }
    }
}