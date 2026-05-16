using System.Linq;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Builders;
using Code.Logic.PlayArea;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.Grid;
using Code.Services.PersistentProgress;
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

        private DiContainer _sceneContainer;
        private GameObject _playZone;

        public GameFactory(
            IInstantiator instantiator,
            IAssetProvider assets,
            IStaticDataService staticData,
            IPersistentProgressService progressService,
            IUnitBuilder unitBuilder)
        {
            _instantiator = instantiator;
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _unitBuilder = unitBuilder;
        }

        public void SetSceneContainer(DiContainer sceneContainer)
        {
            _sceneContainer = sceneContainer;
        }

        public PlayZone CreatePlayZone()
        {
            _playZone = _sceneContainer.InstantiatePrefab(_assets.Load(AssetPath.PlayZonePath));

            GameStaticData gameData = _staticData.GetGameStaticData(MainSceneName);

            CreateGridZone(gameData.PlayerUnitsGridPlacesPosition, UnitTypeId.PlayerUnit);
            CreateGridZone(gameData.EnemiesGridPlacesPosition, UnitTypeId.Enemy);

            return _playZone.GetComponent<PlayZone>();
        }

        public void CreateVictoryConfetti()
        {
            GameObject prefab = _assets.Load(AssetPath.ConfettiVictoryEffectPath);

            GameStaticData gameStaticData = _staticData.GetGameStaticData(MainSceneName);

            for (int i = 0; i < gameStaticData.ConfettiEffectSpawnPointPosition.Count; i++)
            {
                _sceneContainer.InstantiatePrefab(prefab, gameStaticData.ConfettiEffectSpawnPointPosition[i],
                    Quaternion.identity, null);
            }
        }

        private void CreateGridZone(Vector3 position, UnitTypeId unitTypeId)
        {
            GridData gridData = _staticData.GetGridPlaces(unitTypeId).GridData;

            var gridZone = _sceneContainer.InstantiatePrefab(gridData.GridsRoot);

            gridZone.transform.SetParent(_playZone.transform, true);
            gridZone.transform.localPosition = position;
            gridZone.name = unitTypeId.ToString();

            GenerateCellsForGrid(gridZone, gridZone.GetComponent<IGridUnitPlaces>(), unitTypeId);
        }

        private void GenerateCellsForGrid(GameObject gridZone, IGridUnitPlaces gridUnitPlaces,
            UnitTypeId unitTypeId)
        {
            GridData gridData = _staticData.GetGridPlaces(unitTypeId).GridData;

            int gridWidth = gridData.Height;
            int gridHeight = gridData.Width;
            float gridSpacing = gridData.Spacing;

            int index = 0;

            for (int verical = 0; verical < gridWidth; verical++)
            {
                for (int horizontal = 0; horizontal < gridHeight; horizontal++)
                {
                    GameObject cell = _sceneContainer.InstantiatePrefab(gridData.CellPrefab);

                    Vector3 localPosition = new Vector3(horizontal * gridSpacing, 0, verical * gridSpacing);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = localPosition;
                    cell.name = $"Cell {horizontal} {verical}";

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