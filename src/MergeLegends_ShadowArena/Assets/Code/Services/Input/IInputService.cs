using System;
using UnityEngine;

namespace Code.Services.Input
{
  public interface IInputService
  {
    event Action<Ray> OnRay;
    void InitCamera(Camera mainCamera);
  }
}