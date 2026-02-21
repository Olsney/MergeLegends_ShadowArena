using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.Grid;
using Code.Services.StaticData;
using Code.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.Factory.Game
{
    public class GameFactory : IGameFactory
    {
        private static readonly Quaternion PlayerUnitCreateRotation = Quaternion.Euler(0, 180, 0);

        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticDataService;

        private GameObject _playZone;

        public GameFactory(IInstantiator instantiator, IAssetProvider assets, IStaticDataService staticDataService)
        {
            _instantiator = instantiator;
            _assets = assets;
            _staticDataService = staticDataService;
        }

        public GameObject CreatePlayZone(Vector3 playerUnitGrdGridZone, Vector3 enemiesGridZone)
        {
            _playZone = _instantiator.InstantiatePrefab(_assets.Load(AssetPath.PlayZonePath));

            CreateGridZone(playerUnitGrdGridZone, GridPlacesTypeId.PlayerUnit);
            CreateGridZone(enemiesGridZone, GridPlacesTypeId.Enemy);

            return _playZone;
        }

        private void CreateGridZone(Vector3 position, GridPlacesTypeId gridPlacesTypeId)
        {
            var gridZone =
                _instantiator.InstantiatePrefab(_staticDataService.GetGridPlaces(gridPlacesTypeId).GridConfig
                    .GridsRoot);

            gridZone.transform.SetParent(_playZone.transform, true);
            gridZone.transform.localPosition = position;
            gridZone.name = gridPlacesTypeId.ToString();

            GenerateCellsForGrid(gridZone, gridZone.GetComponent<IGridUnitPlaces>(), gridPlacesTypeId);
        }

        private void GenerateCellsForGrid(GameObject gridZone, IGridUnitPlaces gridUnitPlaces,
            GridPlacesTypeId gridPlacesTypeId)
        {
            int width = _staticDataService.GetGridPlaces(gridPlacesTypeId).GridConfig.Width;
            int height = _staticDataService.GetGridPlaces(gridPlacesTypeId).GridConfig.Height;
            float spacing = _staticDataService.GetGridPlaces(gridPlacesTypeId).GridConfig.Spacing;

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GameObject cell = _instantiator.InstantiatePrefab(_staticDataService.GetGridPlaces(gridPlacesTypeId)
                        .GridConfig.CellPrefab);

                    Vector3 localPosition = new Vector3(x * spacing, 0, z * spacing);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = localPosition;
                    cell.name = $"Cell {x} {z}";

                    gridUnitPlaces.AddCell(cell.GetComponent<ICellGrid>());
                }
            }
        }

        public GameObject CreatePlayerUnit(Vector3 position)
        {
            PlayerUnitStaticData playerUnitStaticData = _staticDataService.GetPlayerUnit(PlayerUnitTypeId.Lvl1);

            GameObject playerUnit = _instantiator.InstantiatePrefab(
                playerUnitStaticData.playerUnitConfig.PlayerUnitPrefab, position,
                PlayerUnitCreateRotation, null);

            return playerUnit;
        }
    }
}