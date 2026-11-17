using Code.StaticData;
using UnityEngine;

namespace Code.Units.Audio
{
    public class MeleeAttackSound : MonoBehaviour
    {
        private AudioSource _audioSource;
        private PitchRangeData _pitchRange;

        private void Awake()
            => _audioSource = GetComponent<AudioSource>();

        public void Init(MeleeAttackSoundData soundData)
        {
            _audioSource.clip = soundData.hitAudio.AudioClip;
            _audioSource.volume = soundData.hitAudio.Volume;
            _pitchRange = soundData.pitchRange;
        }

        public void PlayHitSound()
        {
            _audioSource.Stop();
            _audioSource.pitch = Random.Range(_pitchRange.Min, _pitchRange.Max);
            _audioSource.PlayOneShot(_audioSource.clip, _audioSource.volume);
        }
    }
}