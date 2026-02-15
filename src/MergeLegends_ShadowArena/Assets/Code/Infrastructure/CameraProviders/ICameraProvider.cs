using UnityEngine;

namespace Code.Infrastructure.CameraProviders
{
    public interface ICameraProvider
    {
        Camera MainCamera { get; }
        void SetCamera(Camera camera);
    }
}