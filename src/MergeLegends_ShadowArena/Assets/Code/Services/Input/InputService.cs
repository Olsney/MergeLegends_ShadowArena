using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Services.Input
{
    public abstract class InputService : IInputService, IDisposable, IInitializable
    {
        private readonly PlayerInput _playerInput;

        public event Action<Vector2> Touched;

        public InputService()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        public void Initialize()
            => _playerInput.Player.Touch.performed += OnTouchPerformed;

        public void Dispose()
        {
            _playerInput.Disable();
            _playerInput.Player.Touch.performed -= OnTouchPerformed;
        }

        protected abstract Vector2 GetTouchPosition();

        private void OnTouchPerformed(InputAction.CallbackContext context)
            => Touched?.Invoke(GetTouchPosition());
    }
}