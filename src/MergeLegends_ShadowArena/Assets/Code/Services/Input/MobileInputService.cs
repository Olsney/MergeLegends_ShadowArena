using System;
using UnityEngine;

namespace Code.Services.Input
{
  public class MobileInputService : IInputService
  {
    public event Action<Ray> OnRay;
    public void InitCamera(Camera mainCamera)
    {
      throw new NotImplementedException();
    }
  }
}