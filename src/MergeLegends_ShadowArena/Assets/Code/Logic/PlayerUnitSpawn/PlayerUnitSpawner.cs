using Code.Infrastructure.Factory.Game;
using Code.Logic.PlayArea.Cell;

namespace Code.Logic.PlayerUnitSpawn
{
    public class PlayerUnitSpawner : IPlayerUnitSpawner
    {
        private readonly IGameFactory _gameFactory;
        private readonly ICellsProvider _cellsProvider;

        public PlayerUnitSpawner(IGameFactory gameFactory, ICellsProvider cellsProvider)
        {
            _gameFactory = gameFactory;
            _cellsProvider = cellsProvider;
        }

        public void Spawn()
        {
            foreach (var cell in _cellsProvider.GetFreeCells())
            {
                cell.SetUnit(_gameFactory.CreatePlayerUnit(cell.Transform.position));
                return;
            }
        }
    }
}