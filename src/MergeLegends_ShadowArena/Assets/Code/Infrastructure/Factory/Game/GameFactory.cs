using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.Grid;
using Code.Services.SceneProvider;
using Code.Services.StaticData;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory.Game
{
    public class GameFactory : IGameFactory
    {
        private const string MainSceneName = "Main";

        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly ISceneProvider _sceneProvider;

        public GameFactory(
            IInstantiator instantiator,
            IAssetProvider assets,
            IStaticDataService staticData,
            ISceneProvider sceneProvider)
        {
            _instantiator = instantiator;
            _assets = assets;
            _staticData = staticData;
            _sceneProvider = sceneProvider;
        }

        public GameObject CreatePlayZone(LocationTypeId locationTypeId)
        {
            GameObject playZone =
                _instantiator.InstantiatePrefab(_assets.Load(AssetPath.PlayZonePath), _sceneProvider.Container);

            LocationSceneEntryData location = _staticData.GetLocation(locationTypeId);

            CreateGridZone(playZone.transform, location.PlayerUnitsGridPlacesPosition, UnitTypeId.PlayerUnit);
            CreateGridZone(playZone.transform, location.EnemiesGridPlacesPosition, UnitTypeId.Enemy);

            return playZone;
        }

        public void CreateVictoryConfetti(LocationTypeId locationTypeId)
        {
            var confettiPrefab = _assets.Load(AssetPath.ConfettiVictoryEffectPath);
            var spawnPoints = _staticData.GetLocation(locationTypeId).ConfettiEffectSpawnPointPosition;

            for (int i = 0; i < spawnPoints.Count; i++)
            {
                _instantiator.InstantiatePrefab(confettiPrefab, spawnPoints[i], Quaternion.identity,
                    _sceneProvider.Container);
            }
        }

        private void CreateGridZone(Transform parent, Vector3 position, UnitTypeId unitTypeId)
        {
            var gridData = _staticData.GetGridPlaces(unitTypeId).GridData;
            var gridZone = _instantiator.InstantiatePrefab(gridData.GridsRoot, parent);

            gridZone.transform.SetParent(parent, true);
            gridZone.transform.localPosition = position;
            gridZone.name = unitTypeId.ToString();

            GenerateCellsForGrid(gridZone, gridZone.GetComponent<IGridUnitPlaces>(), unitTypeId);
        }

        private void GenerateCellsForGrid(GameObject gridZone, IGridUnitPlaces gridUnitPlaces, UnitTypeId unitTypeId)
        {
            var gridData = _staticData.GetGridPlaces(unitTypeId).GridData;
            var cellPrefab = gridData.CellPrefab;
            var gridWidth = gridData.Height;
            var gridHeight = gridData.Width;
            var spacing = gridData.Spacing;

            for (int vertical = 0; vertical < gridWidth; vertical++)
            {
                for (int horizontal = 0; horizontal < gridHeight; horizontal++)
                {
                    var cell = _instantiator.InstantiatePrefab(cellPrefab, _sceneProvider.Container);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = new Vector3(horizontal * spacing, 0, vertical * spacing);
                    cell.name = $"Cell {horizontal} {vertical}";

                    gridUnitPlaces.AddCell(cell.GetComponent<ICellGrid>());
                }
            }
        }
    }
}