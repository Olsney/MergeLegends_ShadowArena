using Code.Logic.UnitSpawn;
using Zenject;

namespace Code.Logic.PlayArea
{
    public class GridHeroesPlaces : GridUnitPlaces
    {
        [Inject]
        public void Construct(IUnitSpawner unitSpawner)
        {
            unitSpawner.InitiaileCellGrids(CellGrids);
        }
    }
}