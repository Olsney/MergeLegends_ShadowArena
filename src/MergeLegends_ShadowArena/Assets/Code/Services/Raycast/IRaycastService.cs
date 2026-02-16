using System;
using UnityEngine;

namespace Code.Services.Raycast
{
    public interface IRaycastService
    {
        event Action<RaycastHit> HitPerformed;
    }
}