using Code.Infrastructure.Factory.Game;
using Code.Logic.PlayArea.Cell;
using UnityEngine;

namespace Code.Logic.HeroSpawn
{
    public class HeroSpawner : IHeroSpawner
    {
        private readonly IGameFactory _gameFactory;
        private readonly ICellsProvider _cellsProvider;

        public HeroSpawner(IGameFactory gameFactory, ICellsProvider cellsProvider)
        {
            _gameFactory = gameFactory;
            _cellsProvider = cellsProvider;
        }

        public void Spawn()
        {
            foreach (var cell in _cellsProvider.GetFreeCells())
            {
                cell.PlaceUnit(_gameFactory.CreateHero(Quaternion.Euler(0, 180, 0)));
                return;
            }
        }
    }
}