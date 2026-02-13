using System;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InputService : IInputService, IDisposable
{
    private PlayerInput _playerInput;
    protected Camera MainCamera;

    public event Action<Ray> OnRay;

    public InputService()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player.Touch.performed += OnButtonClick;
    }

    public void InitCamera(Camera mainCamera)
        => MainCamera = mainCamera;

    public void Dispose()
    {
        _playerInput.Disable();
        _playerInput.Player.Touch.performed -= OnButtonClick;
    }

    protected abstract void OnButtonClick(InputAction.CallbackContext context);

    protected void InvokeOnRay(Ray ray)
    {
        OnRay?.Invoke(ray);
    }
}