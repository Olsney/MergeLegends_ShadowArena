using UnityEngine;

namespace Code.BaseEffect
{
    public class BaseEffect : MonoBehaviour
    {
        private ParticleSystem _particles;

        private void Awake()
        {
            _particles = GetComponent<ParticleSystem>();
        }

        public float GetDuration() 
            => _particles.main.duration;

        public void Play()
        {
            gameObject.SetActive(true);
            _particles.Play();
        }

        public void Stop()
        {
            gameObject.SetActive(false);
            _particles.Stop();
        }
    }
}