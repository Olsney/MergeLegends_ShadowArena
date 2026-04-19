using Code.Infrastructure.CameraProviders;
using UnityEngine;
using Zenject;

namespace Code.Units.UI
{
    public class LookAtCanvasToCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        [Inject]
        private void Construct(ICameraProvider provider)
        {
            _mainCamera = provider.MainCamera;
        }

        private void Start()
            => LookAtToCamera();

        public void Tick()
            => LookAtToCamera();

        private void LookAtToCamera()
            => transform.LookAt(transform.position + _mainCamera.transform.forward, Vector3.up);
    }
}