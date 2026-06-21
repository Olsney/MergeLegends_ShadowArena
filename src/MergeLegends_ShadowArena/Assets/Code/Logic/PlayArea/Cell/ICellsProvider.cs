using System.Collections.Generic;

namespace Code.Logic.PlayArea.Cell
{
    public interface ICellsProvider
    {
        IEnumerable<ICellGrid> GetFreeCells();
        IEnumerable<ICellGrid> GetAllCells();
        void AddCell(ICellGrid cellGrid);
        void Clear();
    }
}