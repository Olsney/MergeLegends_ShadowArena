using Code.StaticData;
using UnityEngine;

namespace Code.Units.Audio
{
    public class BossAttackSound : MonoBehaviour
    {
        private AudioSource _audioSource;
        private BossAttackSoundData _soundData;

        private void Awake()
            => _audioSource = GetComponent<AudioSource>();

        public void Init(BossAttackSoundData soundData)
            => _soundData = soundData;

        public void PlayAttackSound()
            => Play(_soundData.clubSwingAudio, _soundData.clubSwingPitch);

        public void PlayLastAttackSound()
            => Play(_soundData.groundSlamAudio, _soundData.groundSlamPitch);

        private void Play(AudioData audioData, float pitch)
        {
            _audioSource.pitch = pitch;
            _audioSource.PlayOneShot(audioData.AudioClip, audioData.Volume);
        }
    }
}