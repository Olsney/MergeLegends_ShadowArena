using System.Collections.Generic;

namespace Code.Logic.PlayArea.Cell
{
    public interface ICellsProvider
    {
        IEnumerable<ICellGrid> GetFreeCells();
        IEnumerable<ICellGrid> GetAllCells();
        void InitializeCellGrids(List<ICellGrid> cellGrids);
    }
}