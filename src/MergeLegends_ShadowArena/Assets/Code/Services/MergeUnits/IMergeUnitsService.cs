using System;
using Code.Logic.PlayArea.Cell;

namespace Code.Services.MergeUnits
{
    public interface IMergeUnitsService
    {
        event Action<CellGridPlayerUnit> SelectionChanged;
    }
}