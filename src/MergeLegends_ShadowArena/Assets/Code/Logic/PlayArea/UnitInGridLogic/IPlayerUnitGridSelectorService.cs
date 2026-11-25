using System;
using Code.Logic.PlayArea.Cell;
using UnityEngine;

namespace Code.Logic.PlayArea.UnitInGridLogic
{
    public interface IPlayerUnitGridSelectorService
    {
        void ClearSelection();
        event Action<GameObject, Vector3> UnitCellChanged;
        event Action<CellGridPlayerUnit> UnitSelected;
        event Action SelectionCleared;
    }
}