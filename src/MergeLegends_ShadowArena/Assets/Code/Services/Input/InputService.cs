using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace Code.Services.Input
{
    public abstract class InputService : IInputService, IDisposable, IInitializable
    {
        private readonly PlayerInput _playerInput;
        private readonly PointerEventData _pointerEventData;
        private readonly List<RaycastResult> _raycastResults = new();

        public event Action<Vector2> Touched;

        public InputService()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _pointerEventData = new PointerEventData(EventSystem.current);
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
        {
            Vector2 position = GetTouchPosition();

            if (IsPointerOverUI(position))
                return;

            Touched?.Invoke(position);
        }

        private bool IsPointerOverUI(Vector2 screenPosition)
        {
            if (EventSystem.current == null)
                return false;

            _pointerEventData.position = screenPosition;
            _raycastResults.Clear();

            EventSystem.current.RaycastAll(_pointerEventData, _raycastResults);

            return _raycastResults.Count > 0;
        }
    }
}