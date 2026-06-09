using UnityEngine;
using Zenject;

namespace Code.Infrastructure.CameraProviders
{
    [RequireComponent(typeof(Camera))]
    public class CameraRegister : MonoBehaviour
    {
        private Camera _mainCamera;
        private ICameraProvider _cameraProvider;

        private void Awake()
        {
            _mainCamera = GetComponent<Camera>();
            _cameraProvider.SetCamera(_mainCamera);
        }

        [Inject]
        public void Construct(ICameraProvider cameraProvider)
            => _cameraProvider = cameraProvider;
    }
}