using Code.Infrastructure.Factory.Game;
using Code.Logic.PlayArea.Cell;

namespace Code.UI
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

        public void SpawnMelee()
        {
            foreach (var cell in _cellsProvider.GetFreeCells())
            {
                cell.SetUnit(_gameFactory.CreateMeleePlayerUnit(cell.Transform.position));
                return;
            }
        } 
        
        public void SpawnRanged()
        {
            foreach (var cell in _cellsProvider.GetFreeCells())
            {
                cell.SetUnit(_gameFactory.CreateRangerPlayerUnit(cell.Transform.position));
                return;
            }
        }
    }
}