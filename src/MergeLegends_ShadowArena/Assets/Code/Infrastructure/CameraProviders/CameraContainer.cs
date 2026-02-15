using System;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.CameraProviders
{
    public class CameraContainer : MonoBehaviour
    {
        private Camera _mainCamera;

        [Inject]
        public void Construct(ICameraProvider cameraProvider)
        {
            if (cameraProvider == null)
                throw new NullReferenceException("cameraProvider");

            if (_mainCamera == null)
                _mainCamera = GetComponent<Camera>();

            cameraProvider.SetCamera(_mainCamera);
        }
    }
}