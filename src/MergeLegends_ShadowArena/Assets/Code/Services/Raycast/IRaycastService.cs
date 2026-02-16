using System;
using UnityEngine;

namespace Code.Services.RaycastReader
{
    public interface IRaycastService
    {
        event Action<RaycastHit> HitPerformed;
        void PerformRaycast(Vector2 screenPoint);
    }
}
