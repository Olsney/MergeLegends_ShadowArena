using System.Collections.Generic;
using Code.Infrastructure.Factory.Game;
using Code.Logic.PlayArea.Cell;
using UnityEngine;

namespace Code.Logic.HeroSpawn
{
    public class HeroSpawner : IHeroSpawner
    {
        private readonly IGameFactory _gameFactory;
        private List<CellGridHero> _cellGrids = new List<CellGridHero>();

        public HeroSpawner(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void InitializeCellGrids(List<CellGridHero> cellGrids)
            => _cellGrids = cellGrids;

        public void Spawn()
        {
            for (int i = 0; i < _cellGrids.Count; i++)
            {
                if (_cellGrids[i].IsOccupied == false)
                {
                    _cellGrids[i].PlaceUnit(_gameFactory.CreateHero(Quaternion.Euler(0, 180, 0)));

                    return;
                }
            }
        }
    }
}