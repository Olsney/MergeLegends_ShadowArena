using UnityEngine;

namespace Code.Logic.PlayArea.Cell
{
    public interface ICellGrid
    {
        Transform Transform { get; }
        GameObject Unit { get; }
        bool IsOccupied { get; }
        void SetUnit(GameObject unit);
        void RemoveUnit();
    }
}