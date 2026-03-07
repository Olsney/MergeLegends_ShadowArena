using System.Collections.Generic;

namespace Code.Logic.PlayArea.Cell
{
    public class CellsProvider : ICellsProvider 
    {
        private List<ICellGrid> _allCells = new List<ICellGrid>();
    
        public IEnumerable<ICellGrid> GetFreeCells()
        {
            foreach (var cell in _allCells)
                if (cell.IsOccupied == false)
                    yield return cell;
        }
    
        public IEnumerable<ICellGrid> GetAllCells() 
            => _allCells;
        
        public void AddCell(ICellGrid cellGrid) 
            => _allCells.Add(cellGrid);
    }
}