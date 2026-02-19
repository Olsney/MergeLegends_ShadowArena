using System.Collections.Generic;
using UnityEngine;

namespace Code.Logic.PlayArea
{
    public class GridUnitPlaces : MonoBehaviour
    {
        protected readonly List<CellGridHero> CellGrids = new List<CellGridHero>();

        public void AddCell(CellGridHero cellGridHero)
            => CellGrids.Add(cellGridHero);
    }
}