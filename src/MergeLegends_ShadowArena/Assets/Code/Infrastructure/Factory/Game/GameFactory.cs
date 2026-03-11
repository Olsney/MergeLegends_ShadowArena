using System;
using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayArea;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.Grid;
using Code.Services.StaticData;
using Code.StaticData;
using Code.Units;
using Code.Units.Attack;
using Code.Units.MagicSphereAttack;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Infrastructure.Factory.Game
{
    public class GameFactory : IGameFactory
    {
        private const float EnemySpawnRotationY = 180f;

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

        public PlayZone CreatePlayZone()
        {
            _playZone = _instantiator.InstantiatePrefab(_assets.Load(AssetPath.PlayZonePath));

            LevelStaticData levelData = _staticData.GetLevel(SceneManager.GetActiveScene().name);

            CreateGridZone(levelData.PlayerUnitsGridPlacesPosition, GridPlacesTypeId.PlayerUnit);
            CreateGridZone(levelData.EnemiesGridPlacesPosition, GridPlacesTypeId.Enemy);

            return _playZone.GetComponent<PlayZone>();
        }

        public GameObject CreateUnit(UnitTypeId unitTypeId, UnitAttackTypeId unitAttackTypeId,
            int unitCurrentLevelIndex, Vector3 position)
        {
            if (unitTypeId == UnitTypeId.PlayerUnit)
            {
                return CreateConcreteUnit(_staticData.GetPlayerUnit(unitAttackTypeId), unitCurrentLevelIndex, position,
                    Quaternion.identity,
                    CreateMagicSphere(UnitTypeId.PlayerUnit, position).GetComponent<MagicSphere>());
            }
            else
            {
                return CreateConcreteUnit(_staticData.GetEnemy(unitAttackTypeId), unitCurrentLevelIndex, position,
                    Quaternion.Euler(0, EnemySpawnRotationY, 0),
                    CreateMagicSphere(UnitTypeId.Enemy, position).GetComponent<MagicSphere>());
            }
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

        private void GenerateCellsForGrid(GameObject gridZone, IGridUnitPlaces gridUnitPlaces,
            GridPlacesTypeId gridPlacesTypeId)
        {
            GridData gridData = _staticData.GetGridPlaces(gridPlacesTypeId).GridData;

            int gridWidth = gridData.Height;
            int gridHeight = gridData.Width;
            float gridSpacing = gridData.Spacing;

            for (int verical = 0; verical < gridWidth; verical++)
            {
                for (int horizontal = 0; horizontal < gridHeight; horizontal++)
                {
                    GameObject cell = _instantiator.InstantiatePrefab(gridData.CellPrefab);

                    Vector3 localPosition = new Vector3(horizontal * gridSpacing, 0, verical * gridSpacing);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = localPosition;
                    cell.name = $"Cell {horizontal} {verical}";

                    gridUnitPlaces.AddCell(cell.GetComponent<ICellGrid>());
                }
            }
        }

        private GameObject CreateConcreteUnit(UnitStaticData unitStaticData,
            int unitCurrentLevelIndex, Vector3 position, Quaternion rotation, MagicSphere magicSphere)
        {
            UnitLevelStatsData unitLevelStatsData = unitStaticData.GetStatsLvl(unitCurrentLevelIndex);
            TargetData targetData = unitStaticData.GeneralData.TargetData;
            UnitData unitData = unitStaticData.GeneralData.UnitData;
            MagicSphereData magicSphereData = unitStaticData.GeneralData.MagicSphereData;

            GameObject playerUnit = _instantiator.InstantiatePrefab(
                unitLevelStatsData.ViewPrefab, position,
                rotation, null);

            playerUnit.GetComponent<IHealth>().Init(unitLevelStatsData.Hp);
            playerUnit.GetComponent<ITargetFinder>().Init(targetData.TargetLayerMask);
            playerUnit.GetComponent<UnitNavmeshAgentMover>()
                .InitConfig(unitData.MoveSpeed, unitData.AttackDistance);

            var attacker = playerUnit.GetComponentInChildren<IAttacker>();

            if (attacker is MeleeAttacker meleeAttacker)
            {
                meleeAttacker.Init(unitLevelStatsData.Damage, unitData.AttackCooldown,
                    targetData.TargetLayerMask);
            }
            else if (attacker is RangedAttacker rangedAttacker)
            {
                rangedAttacker.Init(unitLevelStatsData.Damage, unitData.AttackCooldown,
                    targetData.TargetLayerMask, magicSphereData.Speed, playerUnit.GetComponent<UnitDeath>(),
                    magicSphere);
            }

            return playerUnit;
        }

        private GameObject CreateMagicSphere(UnitTypeId unitTypeId, Vector3 position)
        {
            UnitStaticData unitData = null;

            if (unitTypeId == UnitTypeId.PlayerUnit)
            {
                unitData = _staticData.GetPlayerUnit(UnitAttackTypeId.Ranged);
            }
            else if (unitTypeId == UnitTypeId.Enemy)
            {
                unitData = _staticData.GetEnemy(UnitAttackTypeId.Ranged);
            }

            if (unitData == null)
                throw new NullReferenceException("Unit data is null");

            GameObject magicSphere = _instantiator.InstantiatePrefab(
                unitData.GeneralData.MagicSphereData.ViewPrefab,
                position,
                Quaternion.identity, null);

            return magicSphere;
        }
    }
}