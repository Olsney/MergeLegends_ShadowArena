using System;
using UnityEngine;

namespace Code.Logic.PlayArea.Cell
{
    public class CellGridEnemy : MonoBehaviour, ICellGrid
    {
        public Transform Transform => transform;
        public GameObject Unit { get; private set; }
        public bool IsOccupied { get; private set; }
        public int CellIndex { get; private set; }

        public void Init(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Cell index cannot be negative");

            CellIndex = index;
        }
        
        public void SetUnit(GameObject unit)
        {
            Unit = unit;
            IsOccupied = true;
        }

        public void RemoveUnit()
        {
            Unit.transform.SetParent(null);
            Unit = null;
            IsOccupied = false;
        }
    }
}