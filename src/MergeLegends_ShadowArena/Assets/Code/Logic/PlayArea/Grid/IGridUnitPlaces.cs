using System.Collections.Generic;
using Code.Logic.PlayArea.Cell;

namespace Code.Logic.PlayArea.Grid
{
    public interface IGridUnitPlaces
    {
        List<ICellGrid> CellGrids { get; }
        void AddCell(ICellGrid cellGrid);
    }
}