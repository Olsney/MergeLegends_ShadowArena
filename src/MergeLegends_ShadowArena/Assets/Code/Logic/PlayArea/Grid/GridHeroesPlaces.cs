using System.Collections.Generic;
using Code.Logic.HeroSpawn;
using Code.Logic.PlayArea.Cell;
using UnityEngine;
using Zenject;

namespace Code.Logic.PlayArea.Grid
{
    public class GridHeroesPlaces : MonoBehaviour, IGridUnitPlaces
    {
        public List<ICellGrid> CellGrids { get; private set; } = new List<ICellGrid>();

        [Inject]
        private void Construct(ICellsProvider cellsProvider) 
            => cellsProvider.InitializeCellGrids(CellGrids);

        public void AddCell(ICellGrid cellGrid)
        {
            CellGrids.Add(cellGrid);
        }
    }
}