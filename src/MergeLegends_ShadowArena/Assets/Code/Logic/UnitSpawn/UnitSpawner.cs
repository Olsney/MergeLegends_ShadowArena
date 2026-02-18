using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayArea;
using UnityEngine;
using Zenject;

namespace Code.Logic.UnitSpawn
{
    public class UnitSpawner : IUnitSpawner
    {
        private readonly IAssetProvider _assets;
        private readonly IInstantiator _instantiator;
        
        private List<CellGrid>  _cellGrids = new  List<CellGrid>();
        private GameObject _heroLvl1Prefab;

        public UnitSpawner(IAssetProvider assets, IInstantiator instantiator)
        {
            _assets = assets;
            _instantiator = instantiator;
            _heroLvl1Prefab = _assets.Load(AssetPath.HeroLvl1);
        }

        public void InitiaileCellGrids(List<CellGrid> cellGrids)
        {
            _cellGrids = cellGrids;
        }
        
        public void Spawn()
        {
            for (int i = 0; i < _cellGrids.Count; i++)
            {
                if (_cellGrids[i].IsOccupied)
                {
                    GameObject heroPrefab = _instantiator.InstantiatePrefab(_heroLvl1Prefab);
                    heroPrefab.transform.position = _cellGrids[i].transform.position;
                    heroPrefab.transform.Rotate(0, 180, 0);
                    _cellGrids[i].PlaceUnit(heroPrefab);
                    return;
                }
            }
            
        }
    }
}