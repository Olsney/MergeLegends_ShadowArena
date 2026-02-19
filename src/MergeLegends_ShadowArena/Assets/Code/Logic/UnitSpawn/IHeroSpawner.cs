using System.Collections.Generic;
using Code.Logic.PlayArea;
using Code.Logic.PlayArea.Cell;

namespace Code.Logic.UnitSpawn
{
    public interface IHeroSpawner
    {
        void InitializeCellGrids(List<CellGridHero> cellGrids);
        void Spawn();
    }
}