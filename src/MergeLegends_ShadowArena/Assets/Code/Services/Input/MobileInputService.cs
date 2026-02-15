using UnityEngine.InputSystem;

namespace Code.Services.Input
{
    public class MobileInputService : InputService
    {
        protected override void OnButtonClick(InputAction.CallbackContext context)
        {
            InvokeOnRay(MainCamera.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue()));
        }
    }
}