using System.Collections.Generic;
using Code.Logic.PlayArea.Cell;
using UnityEngine;

namespace Code.Logic.PlayArea.Grid
{
    public class GridEnemiesPlaces : MonoBehaviour, IGridUnitPlaces
    {
        public List<ICellGrid> CellGrids { get; private set; } = new List<ICellGrid>();

        public void AddCell(ICellGrid cellGrid)
        {
            CellGrids.Add(cellGrid);
        }
    }
}