using System;
using Code.Infrastructure.CameraProviders;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Services.Raycast
{
    public class RaycastService : IDisposable, IInitializable, IRaycastService
    {
        private const string LayerMaskName = "CellGrid";
        
        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;
        private readonly int _layerMask;

        public event Action<RaycastHit> HitPerformed;

        public RaycastService(IInputService inputService, ICameraProvider cameraProvider)
        {
            _inputService = inputService;
            _cameraProvider = cameraProvider;
            _layerMask = 1 << LayerMask.NameToLayer(LayerMaskName);
        }

        public void Initialize()
            => _inputService.Touched += PerformRaycast;

        public void Dispose()
            => _inputService.Touched -= PerformRaycast;

        private void PerformRaycast(Vector2 screenPoint)
        {
            if (_cameraProvider == null || _cameraProvider.MainCamera == null)
                return;
            
            Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(screenPoint);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                HitPerformed?.Invoke(hit);
            }
        }
    }
}