using UnityEngine;

namespace Code.Services.CameraMoves
{
    public interface ICameraShakeService
    {
        void StartVerticleShake(float force, Vector2 direction);
    }
}