using System;
using Code.Infrastructure.CameraProviders;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Services.Raycast
{
    public class RaycastService : IDisposable, IInitializable, IRaycastService
    {
        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;

        public event Action<RaycastHit> HitPerformed;

        public RaycastService(IInputService inputService, ICameraProvider cameraProvider)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
        }

        public void Initialize()
            => _inputService.Touched += PerformRaycast;

        public void Dispose()
            => _inputService.Touched -= PerformRaycast;

        private void PerformRaycast(Vector2 screenPoint)
        {
            Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(screenPoint);

            if (Physics.Raycast(ray, out RaycastHit hit))
                HitPerformed?.Invoke(hit);
        }
    }
}