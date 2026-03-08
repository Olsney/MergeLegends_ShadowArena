using System.Collections.Generic;
using Code.Logic.PlayArea.Cell;
using UnityEngine;
using Zenject;

namespace Code.Logic.PlayArea.Grid
{
    public class GridEnemiesPlaces : MonoBehaviour, IGridUnitPlaces
    {
        private IEnemyCellsProvider _cellsProvider;

        [Inject]
        private void Construct(IEnemyCellsProvider cellsProvider)
            => _cellsProvider = cellsProvider;

        public void AddCell(ICellGrid cellGrid)
            => _cellsProvider.AddCell(cellGrid);
    }
}