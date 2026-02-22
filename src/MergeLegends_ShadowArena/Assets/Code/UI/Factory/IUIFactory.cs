using System;
using UnityEngine;

namespace Code.UI.Factory
{
    public interface IUIFactory
    {
        GameObject CreateUIRoot();
        GameObject CreateHud();
        GameObject CreateSpawnPlayerUnitView();
        event Action UiRootCreated;
    }
}