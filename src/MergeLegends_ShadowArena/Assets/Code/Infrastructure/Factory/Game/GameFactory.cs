using Code.Infrastructure.AssetManagement;
using Code.Logic.PlayArea;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.Grid;
using Code.Logic.PlayArea.GridConfig;
using Code.Services.StaticData;
using Code.StaticData;
using Code.Units;
using Code.Units.Attack;
using Code.Units.Config;
using Code.Units.PlayerUnit.Config;
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

            int gridWidth = gridConfig.Height;
            int gridHeight = gridConfig.Width;
            float gridSpacing = gridConfig.Spacing;

            for (int verical = 0; verical < gridWidth; verical++)
            {
                for (int horizontal = 0; horizontal < gridHeight; horizontal++)
                {
                    GameObject cell = _instantiator.InstantiatePrefab(gridConfig.CellPrefab);

                    Vector3 localPosition = new Vector3(horizontal * gridSpacing, 0, verical * gridSpacing);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = localPosition;
                    cell.name = $"Cell {horizontal} {verical}";

                    gridUnitPlaces.AddCell(cell.GetComponent<ICellGrid>());
                }
            }
        }

        public GameObject CreateMagicSpherePlayerUnit(Vector3 position)
        {
            UnitStaticData unitData = _staticData.GetPlayerUnit(UnitTypeId.Ranged);

            GameObject magicSphere = _instantiator.InstantiatePrefab(
                unitData.GeneralConfig.MagicSphereConfig.ViewPrefab,
                position,
                Quaternion.identity, null);

            return magicSphere;
        }

        public GameObject CreateEnemy(UnitTypeId unitTypeId, UnitLvlTypeId unitLvlTypeId, Vector3 position)
        {
            UnitStaticData unitData = _staticData.GetEnemy(unitTypeId);
            return CreateUnit(position, unitData, unitLvlTypeId);
        }

        public GameObject CreateMeleePlayerUnit(Vector3 position)
        {
            UnitStaticData unitData = _staticData.GetPlayerUnit(UnitTypeId.Melee);
            return CreateUnit(position, unitData, UnitLvlTypeId.Lvl1);
        }

        public GameObject CreateRangerPlayerUnit(Vector3 position)
        {
            UnitStaticData unitData = _staticData.GetPlayerUnit(UnitTypeId.Ranged);
            return CreateUnit(position, unitData, UnitLvlTypeId.Lvl1);
        }

        private GameObject CreateUnit(Vector3 position, UnitStaticData unitData, UnitLvlTypeId unitLvlTypeId)
        {
            UnitLvlStatsConfig lvlStatsConfig = unitData.GetStatsLvl(unitLvlTypeId);
            TargetConfig targetConfig = unitData.GeneralConfig.TargetConfig;
            UnitConfig unitConfig = unitData.GeneralConfig.UnitConfig;
            MagicSphereConfig magicSphereConfig = unitData.GeneralConfig.MagicSphereConfig;

            GameObject playerUnit = _instantiator.InstantiatePrefab(
                lvlStatsConfig.ViewPrefab, position,
                Quaternion.identity, null);

            playerUnit.GetComponent<IHealth>().InitConfig(lvlStatsConfig.Hp);
            playerUnit.GetComponent<ITargetFinder>().InitConfig(targetConfig.TargetLayerMask);
            playerUnit.GetComponent<UnitAgentMover>().InitConfig(unitConfig.MoveSpeed, unitConfig.AttackDistance);

            var attacker = playerUnit.GetComponentInChildren<IAttacker>();

            if (attacker is MeleeAttacker meleeAttacker)
            {
                meleeAttacker.InitConfig(lvlStatsConfig.Damage, unitConfig.AttackCooldown,
                    targetConfig.TargetLayerMask);
            }
            else if (attacker is RangedAttacker rangedAttacker)
            {
                rangedAttacker.InitConfig(lvlStatsConfig.Damage, unitConfig.AttackCooldown,
                    targetConfig.TargetLayerMask, magicSphereConfig.Speed);
            }

            return playerUnit;
        }
    }
}