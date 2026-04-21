using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayArea;
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
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;

        private GameObject _playZone;

        public GameFactory(
            IInstantiator instantiator,
            IAssetProvider assets,
            IStaticDataService staticData)
        {
            _instantiator = instantiator;
            _assets = assets;
            _staticData = staticData;
        }

        public PlayZone CreatePlayZone()
        {
            _playZone = _instantiator.InstantiatePrefab(_assets.Load(AssetPath.PlayZonePath));

            LevelStaticData levelData = _staticData.GetLevel(SceneManager.GetActiveScene().name);

            CreateGridZone(levelData.PlayerUnitsGridPlacesPosition, GridPlacesTypeId.PlayerUnit);
            CreateGridZone(levelData.EnemiesGridPlacesPosition, GridPlacesTypeId.Enemy);

            return _playZone.GetComponent<PlayZone>();
        }

        private void CreateGridZone(Vector3 position, GridPlacesTypeId gridPlacesTypeId)
        {
            GridData gridData = _staticData.GetGridPlaces(gridPlacesTypeId).GridData;

            var gridZone = _instantiator.InstantiatePrefab(gridData.GridsRoot);

            gridZone.transform.SetParent(_playZone.transform, true);
            gridZone.transform.localPosition = position;
            gridZone.name = gridPlacesTypeId.ToString();

            GenerateCellsForGrid(gridZone, gridZone.GetComponent<IGridUnitPlaces>(), gridPlacesTypeId);
        }

        private void GenerateCellsForGrid(
            GameObject gridZone,
            IGridUnitPlaces gridUnitPlaces,
            GridPlacesTypeId gridPlacesTypeId)
        {
            GridData gridData = _staticData.GetGridPlaces(gridPlacesTypeId).GridData;

            int gridWidth = gridData.Height;
            int gridHeight = gridData.Width;
            float gridSpacing = gridData.Spacing;

            for (int vertical = 0; vertical < gridWidth; vertical++)
            {
                for (int horizontal = 0; horizontal < gridHeight; horizontal++)
                {
                    GameObject cell = _instantiator.InstantiatePrefab(gridData.CellPrefab);

                    Vector3 localPosition = new Vector3(horizontal * gridSpacing, 0, vertical * gridSpacing);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = localPosition;
                    cell.name = $"Cell {horizontal} {vertical}";

                    gridUnitPlaces.AddCell(cell.GetComponent<ICellGrid>());
                }
            }
        }
    }
}