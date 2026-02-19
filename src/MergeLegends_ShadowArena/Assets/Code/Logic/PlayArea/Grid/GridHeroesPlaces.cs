using Code.Logic.PlayArea.Cell;
using Code.Logic.UnitSpawn;
using Zenject;

namespace Code.Logic.PlayArea.Grid
{
    public class GridHeroesPlaces : GridUnitPlaces<CellGridHero>
    {
        [Inject]
        public void Construct(IHeroSpawner heroSpawner)
            => heroSpawner.InitializeCellGrids(CellGrids);
    }
}