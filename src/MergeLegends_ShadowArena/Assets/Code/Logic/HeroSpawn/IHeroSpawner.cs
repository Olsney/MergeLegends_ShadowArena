using System.Collections.Generic;
using Code.Logic.PlayArea.Cell;

namespace Code.Logic.HeroSpawn
{
    public interface IHeroSpawner
    {
        void InitializeCellGrids(List<CellGridHero> cellGrids);
        void Spawn();
    }
}