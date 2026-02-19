using System;
using UnityEngine;

namespace Code.UI.Factory
{
  public interface IUIFactory
  {
    GameObject CreateUIRoot();
    GameObject CreateHud();
    GameObject CreateSpawnHeroView();
    event Action UIRootCreated;
    GameObject UiRoot { get; }
  }
}