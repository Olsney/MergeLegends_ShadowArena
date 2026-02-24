using System;
using UnityEngine;

namespace Code.UI.Factory
{
    public interface IUIFactory
    {
        GameObject CreateUIRoot();
        GameObject CreateHud();
        GameObject CreateSpawnPlayerUnitView();
        GameObject CreateStartFightView();
        event Action UiRootCreated;
    }
}