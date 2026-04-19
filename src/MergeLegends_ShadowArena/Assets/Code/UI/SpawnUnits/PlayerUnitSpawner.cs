using Code.Infrastructure.Factory.Game;
using Code.Logic.PlayArea.Cell;
using Code.StaticData;

namespace Code.UI.SpawnUnits
{
    public class PlayerUnitSpawner : IPlayerUnitSpawner
    {
        private const int FirstLevelIndex = 0;
        
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerUnitCellsProvider _cellsProvider;

        public PlayerUnitSpawner(IGameFactory gameFactory, IPlayerUnitCellsProvider cellsProvider)
        {
            _gameFactory = gameFactory;
            _cellsProvider = cellsProvider;
        }

        public void Melee()
        {
            foreach (var cell in _cellsProvider.GetFreeCells())
            {
                cell.SetUnit(_gameFactory.CreateUnit(UnitTypeId.PlayerUnit, UnitAttackTypeId.Melee, FirstLevelIndex,
                    cell.Transform.position));
                return;
            }
        }

        public void Ranged()
        {
            foreach (var cell in _cellsProvider.GetFreeCells())
            {
                cell.SetUnit(_gameFactory.CreateUnit(UnitTypeId.PlayerUnit, UnitAttackTypeId.Ranged, FirstLevelIndex,
                    cell.Transform.position));
                return;
            }
        }
    }
}