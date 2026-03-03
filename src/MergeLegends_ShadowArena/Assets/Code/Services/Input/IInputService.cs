using System;
using Vector2 = UnityEngine.Vector2;

namespace Code.Services.Input
{
    public interface IInputService
    {
        event Action<Vector2> Touched;
    }
}