using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.Grid;
using Code.Logic.PlayArea.GridConfig;
using Code.Services.StaticData;
using Code.StaticData;
using Code.Units;
using Code.Units.PlayerUnit;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.Factory.Game
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        private GameObject _playZone;

        public GameFactory(IInstantiator instantiator, IAssetProvider assets, IStaticDataService staticData)
        {
            _instantiator = instantiator;
            _assets = assets;
            _staticData = staticData;
        }

        public GameObject CreatePlayZone()
        {
            _playZone = _instantiator.InstantiatePrefab(_assets.Load(AssetPath.PlayZonePath));

            LevelStaticData levelData = _staticData.GetLevel(SceneManager.GetActiveScene().name);
            
            CreateGridZone(levelData.PlayerUnitsGridPlacesPosition, GridPlacesTypeId.PlayerUnit);
            CreateGridZone(levelData.EnemiesGridPlacesPosition, GridPlacesTypeId.Enemy);

            return _playZone;
        }

        private void CreateGridZone(Vector3 position, GridPlacesTypeId gridPlacesTypeId)
        {
            GridConfig gridConfig = _staticData.GetGridPlaces(gridPlacesTypeId).GridConfig;

            var gridZone = _instantiator.InstantiatePrefab(gridConfig.GridsRoot);

            gridZone.transform.SetParent(_playZone.transform, true);
            gridZone.transform.localPosition = position;
            gridZone.name = gridPlacesTypeId.ToString();

            GenerateCellsForGrid(gridZone, gridZone.GetComponent<IGridUnitPlaces>(), gridPlacesTypeId);
        }

        private void GenerateCellsForGrid(GameObject gridZone, IGridUnitPlaces gridUnitPlaces,
            GridPlacesTypeId gridPlacesTypeId)
        {
            GridConfig gridConfig = _staticData.GetGridPlaces(gridPlacesTypeId).GridConfig;

            int gridWidth = gridConfig.Width;
            int gridHeight = gridConfig.Height;
            float gridSpacing = gridConfig.Spacing;

            for (int x = 0; x < gridWidth; x++)
            {
                for (int z = 0; z < gridHeight; z++)
                {
                    GameObject cell = _instantiator.InstantiatePrefab(gridConfig.CellPrefab);

                    Vector3 localPosition = new Vector3(x * gridSpacing, 0, z * gridSpacing);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = localPosition;
                    cell.name = $"Cell {x} {z}";

                    gridUnitPlaces.AddCell(cell.GetComponent<ICellGrid>());
                }
            }
        }

        public GameObject CreatePlayerUnit(Vector3 position)
        {
            PlayerUnitStaticData playerUnitStaticData = _staticData.GetPlayerUnit(PlayerUnitTypeId.Lvl1);
            
            GameObject playerUnit = _instantiator.InstantiatePrefab(
                playerUnitStaticData.PlayerUnitConfig.PlayerUnitPrefab, position,
                Quaternion.identity, null);
            
            playerUnit.GetComponent<IHealth>().InitConfig(playerUnitStaticData.PlayerUnitConfig);
            playerUnit.GetComponent<ITargetFinder>().InitConfig(playerUnitStaticData.TargetFinderConfig);
            playerUnit.GetComponent<UnitAgentMover>().InitConfig(playerUnitStaticData.PlayerUnitConfig);
            playerUnit.GetComponentInChildren<MeleeAttacker>().InitConfig(playerUnitStaticData.AttackConfig);
            
            return playerUnit;
        }
    }
}