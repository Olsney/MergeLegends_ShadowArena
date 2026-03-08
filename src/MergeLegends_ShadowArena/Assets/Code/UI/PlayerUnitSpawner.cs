using Code.Infrastructure.Factory.Game;
using Code.Logic.PlayArea.Cell;
using Code.StaticData;

namespace Code.UI
{
    public class PlayerUnitSpawner : IPlayerUnitSpawner
    {
        private readonly IGameFactory _gameFactory;
        private readonly IPlayerUnitCellsProvider _cellsProvider;

        public PlayerUnitSpawner(IGameFactory gameFactory, IPlayerUnitCellsProvider cellsProvider)
        {
            _gameFactory = gameFactory;
            _cellsProvider = cellsProvider;
        }

        public void SpawnMelee()
        {
            foreach (var cell in _cellsProvider.GetFreeCells())
            {
                cell.SetUnit(_gameFactory.CreateUnit(UnitTypeId.PlayerUnit, UnitAttackTypeId.Melee, UnitLvlTypeId.Lvl1,
                    cell.Transform.position));
                return;
            }
        }

        public void SpawnRanged()
        {
            foreach (var cell in _cellsProvider.GetFreeCells())
            {
                cell.SetUnit(_gameFactory.CreateUnit(UnitTypeId.PlayerUnit, UnitAttackTypeId.Ranged, UnitLvlTypeId.Lvl1,
                    cell.Transform.position));
                return;
            }
        }
    }
}