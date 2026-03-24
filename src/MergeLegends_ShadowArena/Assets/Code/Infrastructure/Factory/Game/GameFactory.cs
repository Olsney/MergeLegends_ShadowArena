using System;
using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayArea;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.Grid;
using Code.Services.StaticData;
using Code.StaticData;
using Code.UI;
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
            switch (unitTypeId)
            {
                case UnitTypeId.PlayerUnit:
                    return CreateConcreteUnit(_staticData.GetPlayerUnit(unitAttackTypeId),
                        unitCurrentLevelIndex, position,
                        Quaternion.identity,
                        UnitTypeId.PlayerUnit, position);
                case UnitTypeId.Enemy:
                    return CreateConcreteUnit(_staticData.GetEnemy(unitAttackTypeId),
                        unitCurrentLevelIndex, position,
                        Quaternion.Euler(0, EnemySpawnRotationY, 0), 
                        UnitTypeId.Enemy, position);
                default:
                    throw new ArgumentOutOfRangeException(nameof(unitTypeId), unitTypeId, null);
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
            int unitCurrentLevelIndex, Vector3 position, Quaternion rotation, UnitTypeId unitTypeId,
            Vector3 magicSpherePosition)
        {
            UnitLevelStatsData unitLevelStatsData = unitStaticData.GetStatsLvl(unitCurrentLevelIndex);
            TargetData targetData = unitStaticData.GeneralData.TargetData;
            UnitData unitData = unitStaticData.GeneralData.UnitData;
            MagicSphereData magicSphereData = unitStaticData.GeneralData.MagicSphereData;

            GameObject unit = _instantiator.InstantiatePrefab(
                unitLevelStatsData.ViewPrefab, position,
                rotation, null);

            unit.GetComponent<IHealth>().Init(unitLevelStatsData.Hp);
            unit.GetComponent<ITargetFinder>().Init(targetData.TargetLayerMask);
            unit.GetComponent<UnitNavmeshAgentMover>().InitConfig(unitData.MoveSpeed, unitData.AttackDistance);
            unit.GetComponentInChildren<UnitLevelShower>().Init(unitCurrentLevelIndex);

            var attacker = unit.GetComponentInChildren<IAttacker>();

            switch (attacker)
            {
                case MeleeAttacker melee:
                    melee.Init(unitLevelStatsData.Damage, unitData.AttackCooldown, targetData.TargetLayerMask);
                    break;
                case RangedAttacker ranged:
                    ranged.Init(unitLevelStatsData.Damage, unitData.AttackCooldown, targetData.TargetLayerMask,
                        magicSphereData.Speed, unit.GetComponent<UnitDeath>(),
                        CreateMagicSphere(unitTypeId, magicSpherePosition).GetComponent<MagicSphere>());
                    break;
                default:
                    throw new NullReferenceException($"Unknown attacker type: {attacker}");
            }

            return unit;
        }

        private GameObject CreateMagicSphere(UnitTypeId unitTypeId, Vector3 position)
        {
            UnitStaticData unitStaticData;

            switch (unitTypeId)
            {
                case UnitTypeId.PlayerUnit:
                    unitStaticData = _staticData.GetPlayerUnit(UnitAttackTypeId.Ranged);
                    break;
                case UnitTypeId.Enemy:
                    unitStaticData = _staticData.GetEnemy(UnitAttackTypeId.Ranged);
                    break;
                default:
                    throw new NullReferenceException($"UnitStaticData not found for {unitTypeId}");
            }

            GameObject magicSphere = _instantiator.InstantiatePrefab(
                unitStaticData.GeneralData.MagicSphereData.ViewPrefab,
                position,
                Quaternion.identity, null);

            return magicSphere;
        }
    }
}