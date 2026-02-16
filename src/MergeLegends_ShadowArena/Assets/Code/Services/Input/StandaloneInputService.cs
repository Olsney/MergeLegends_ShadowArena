using UnityEngine.InputSystem;

namespace Code.Services.Input
{
    public class StandaloneInputService : InputService
    {
        protected override void OnButtonClick(InputAction.CallbackContext context)
        {
            InvokeOnRay(MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        }
    }
}