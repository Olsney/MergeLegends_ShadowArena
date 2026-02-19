using System.Collections.Generic;
using Code.Infrastructure.Factory.Game;
using Code.Logic.PlayArea;
using UnityEngine;

namespace Code.Logic.UnitSpawn
{
    public class UnitSpawner : IUnitSpawner
    {
        private readonly IGameFactory _gameFactory;
        private List<CellGridHero> _cellGrids = new List<CellGridHero>();

        public UnitSpawner(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void InitiaileCellGrids(List<CellGridHero> cellGrids)
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