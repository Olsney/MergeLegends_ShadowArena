using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services.Input
{
    public class StandaloneInputService : InputService
    {
        protected override Vector2 GetTouchPosition()
            => Mouse.current.position.ReadValue();
    }
}