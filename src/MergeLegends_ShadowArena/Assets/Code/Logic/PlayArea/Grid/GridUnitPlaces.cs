using System.Collections.Generic;
using Code.Logic.PlayArea.Cell;
using UnityEngine;

namespace Code.Logic.PlayArea.Grid
{
    public class GridUnitPlaces<T> : MonoBehaviour where T : CellGrid
    {
        protected readonly List<T> CellGrids = new List<T>();

        public void AddCell(T cellGrid) 
            => CellGrids.Add(cellGrid);
    }
}