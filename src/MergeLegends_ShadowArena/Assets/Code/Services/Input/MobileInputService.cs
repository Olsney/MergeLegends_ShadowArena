using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Services.Input
{
    public class MobileInputService : InputService
    {
        protected override Vector2 GetTouchPosition() 
            => Touchscreen.current.primaryTouch.position.ReadValue();
    }
}