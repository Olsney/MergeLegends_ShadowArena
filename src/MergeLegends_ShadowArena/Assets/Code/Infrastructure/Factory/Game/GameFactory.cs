using System.Linq;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Builders;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.Grid;
using Code.Services.PersistentProgress;
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
        private readonly IPersistentProgressService _progressService;
        private readonly IUnitBuilder _unitBuilder;
        private readonly ISceneProvider _sceneProvider;

        public GameFactory(
            IInstantiator instantiator,
            IAssetProvider assets,
            IStaticDataService staticData,
            IPersistentProgressService progressService,
            IUnitBuilder unitBuilder,
            ISceneProvider sceneProvider)
        {
            _instantiator = instantiator;
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _unitBuilder = unitBuilder;
            _sceneProvider = sceneProvider;
        }

        public GameObject CreatePlayZone()
        {
            GameObject playZone =
                _instantiator.InstantiatePrefab(_assets.Load(AssetPath.PlayZonePath), _sceneProvider.Container);

            GameStaticData gameData = _staticData.GetGameStaticData(MainSceneName);

            CreateGridZone(playZone.transform, gameData.PlayerUnitsGridPlacesPosition, UnitTypeId.PlayerUnit);
            CreateGridZone(playZone.transform, gameData.EnemiesGridPlacesPosition, UnitTypeId.Enemy);

            return playZone;
        }

        public void CreateVictoryConfetti()
        {
            var confettiPrefab = _assets.Load(AssetPath.ConfettiVictoryEffectPath);
            var spawnPoints = _staticData.GetGameStaticData(MainSceneName).ConfettiEffectSpawnPointPosition;

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

            int index = 0;

            for (int vertical = 0; vertical < gridWidth; vertical++)
            {
                for (int horizontal = 0; horizontal < gridHeight; horizontal++)
                {
                    var cell = _instantiator.InstantiatePrefab(cellPrefab, _sceneProvider.Container);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = new Vector3(horizontal * spacing, 0, vertical * spacing);
                    cell.name = $"Cell {horizontal} {vertical}";

                    gridUnitPlaces.AddCell(cell.GetComponent<ICellGrid>());
                    InitCell(unitTypeId, cell, index);
                    index++;
                }
            }
        }

        private void InitCell(UnitTypeId unitTypeId, GameObject cell, int index)
        {
            switch (unitTypeId)
            {
                case UnitTypeId.PlayerUnit:
                    InitCellPlayerUnit(cell, index);
                    break;
                case UnitTypeId.Enemy:
                    InitCellEnemy(cell, index);
                    break;
            }
        }

        private void InitCellEnemy(GameObject cell, int index)
        {
            cell.GetComponent<CellGridEnemy>().Init(index);
        }

        private void InitCellPlayerUnit(GameObject cell, int index)
        {
            var cellGridPlayerUnit = cell.GetComponent<CellGridPlayerUnit>();
            var savedData = _progressService.Progress.WorldData.CompositionTeamData.PlayerUnitInCells
                .FirstOrDefault(data => data.CellIndex == index);

            if (savedData != null)
            {
                var unit = _unitBuilder
                    .SetUnitType(UnitTypeId.PlayerUnit)
                    .SetUnitAttackType(savedData.UnitAttackTypeId)
                    .SetCurrentLevel(savedData.PlayerUnitLevelIndex)
                    .SetPosition(cell.transform.position)
                    .Build();

                cellGridPlayerUnit.Init(index, unit);
                return;
            }

            cellGridPlayerUnit.Init(index);
        }
    }
}