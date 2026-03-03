using Code.Logic.PlayArea.Cell;
using UnityEngine;
using Zenject;

namespace Code.Logic.PlayArea.Grid
{
    public class GridPlayerUnitPlaces : MonoBehaviour, IGridUnitPlaces
    {
        private ICellsProvider _cellsProvider;

        [Inject]
        private void Construct(ICellsProvider cellsProvider)
            => _cellsProvider = cellsProvider;

        public void AddCell(ICellGrid cellGrid)
            => _cellsProvider.AddCell(cellGrid);
    }
}