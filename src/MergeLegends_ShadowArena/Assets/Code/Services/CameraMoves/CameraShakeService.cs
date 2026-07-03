using System;
using Code.Infrastructure.CameraProviders;
using UnityEngine;

namespace Code.Services.CameraMoves
{
    public class CameraShakeService : ICameraShakeService
    {
        private readonly ICinemachineProvider _cinemachineProvider;

        public CameraShakeService(ICinemachineProvider cinemachineProvider)
        {
            _cinemachineProvider = cinemachineProvider;
        }

        public void StartVerticleShake(float force, Vector2 direction)
        {
            var impulse = _cinemachineProvider.ImpulseSource;

            if (impulse == null)
                throw new ArgumentNullException(nameof(impulse));

            Vector3 dir = new Vector3(direction.x, direction.y, 0f).normalized;

            impulse.GenerateImpulse(dir * force);
        }
    }
}