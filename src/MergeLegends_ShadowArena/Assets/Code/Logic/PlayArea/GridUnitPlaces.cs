using System.Collections.Generic;
using UnityEngine;

namespace Code.Logic.PlayArea
{
    public class GridUnitPlaces : MonoBehaviour
    {
        protected readonly List<CellGrid> CellGrids = new List<CellGrid>();
        
        public void AddCell(CellGrid cellGrid)
        {
            CellGrids.Add(cellGrid);
        }

        public void AddCell(List<CellGrid> cellGrids)
        {
            CellGrids.AddRange(cellGrids);
        }
    }
}