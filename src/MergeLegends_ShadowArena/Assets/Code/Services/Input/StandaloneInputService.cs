using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services.Input
{
    public class StandaloneInputService : IInputService, IDisposable
    {
        private PlayerInput _playerInput;
        private Camera _mainCamera;
        public event Action<Ray> OnRay;

        public StandaloneInputService()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _playerInput.Player.Touch.performed += OnButtonClick;
        }

        public void InitCamera(Camera mainCamera) 
            => _mainCamera = mainCamera;

        public void Dispose()
        {
            _playerInput.Disable();
            _playerInput.Player.Touch.performed -= OnButtonClick;
        }

        private void OnButtonClick(InputAction.CallbackContext context)
        {
            OnRay?.Invoke(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        }
    }
}