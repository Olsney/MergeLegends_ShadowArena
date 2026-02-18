using System.Collections.Generic;
using Code.Logic.PlayArea;

namespace Code.Logic.UnitSpawn
{
    public interface IUnitSpawner
    {
        void Spawn();
        void InitiaileCellGrids(List<CellGrid> cellGrids);
    }
}