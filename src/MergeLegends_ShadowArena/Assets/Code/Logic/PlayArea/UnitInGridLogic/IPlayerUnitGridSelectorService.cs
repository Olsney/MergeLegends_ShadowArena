using System;
using UnityEngine;

namespace Code.Logic.PlayArea.UnitInGridLogic
{
    public interface IPlayerUnitGridSelectorService
    {
        event Action<GameObject, Vector3> UntiCellChanged;
    }
}