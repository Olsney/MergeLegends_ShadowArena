using UnityEngine;

namespace Code.Infrastructure.CameraProviders
{
    public class CameraProvider : ICameraProvider
    {
        public Camera MainCamera { get; private set; }

        public void SetCamera(Camera camera)
            => MainCamera = camera;
    }
}