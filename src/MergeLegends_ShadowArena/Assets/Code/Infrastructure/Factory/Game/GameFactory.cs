using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.States;
using Code.Logic.PlayArea;
using Code.Logic.PlayArea.Cell;
using Code.Logic.PlayArea.Grid;
using Code.Services.StaticData;
using Code.StaticData;
using Code.Units;
using Code.Units.Attack;
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

            int gridWidth = gridData.Width;
            int gridHeight = gridData.Height;
            float gridSpacing = gridData.Spacing;

            for (int x = 0; x < gridWidth; x++)
            {
                for (int z = 0; z < gridHeight; z++)
                {
                    GameObject cell = _instantiator.InstantiatePrefab(gridData.CellPrefab);

                    Vector3 localPosition = new Vector3(x * gridSpacing, 0, z * gridSpacing);
                    cell.transform.SetParent(gridZone.transform, true);
                    cell.transform.localPosition = localPosition;
                    cell.name = $"Cell {x} {z}";

                    gridUnitPlaces.AddCell(cell.GetComponent<ICellGrid>());
                }
            }
        }

        public GameObject CreateMagicSpherePlayerUnit(Vector3 position)
        {
            UnitStaticData unitData = _staticData.GetPlayerUnit(UnitTypeId.Ranged);

            GameObject magicSphere = _instantiator.InstantiatePrefab(
                unitData.GeneralData.MagicSphereData.ViewPrefab,
                position,
                Quaternion.identity, null);

            return magicSphere;
        }

        public GameObject CreateMeleePlayerUnit(Vector3 position)
        {
            UnitStaticData unitData = _staticData.GetPlayerUnit(UnitTypeId.Melee);
            return CreatePlayerUnit(position, unitData);
        }

        public GameObject CreateRangerPlayerUnit(Vector3 position)
        {
            UnitStaticData unitData = _staticData.GetPlayerUnit(UnitTypeId.Ranged);
            return CreatePlayerUnit(position, unitData);
        }

        private GameObject CreatePlayerUnit(Vector3 position, UnitStaticData unitStaticData)
        {
            UnitLvlStatsData lvlStatsData = unitStaticData.GetStatsLvl(UnitLvlTypeId.Lvl1);
            TargetData targetData = unitStaticData.GeneralData.TargetData;
            UnitData unitData = unitStaticData.GeneralData.UnitData;
            MagicSphereData magicSphereData = unitStaticData.GeneralData.MagicSphereData;

            GameObject playerUnit = _instantiator.InstantiatePrefab(
                lvlStatsData.ViewPrefab, position,
                Quaternion.identity, null);

            playerUnit.GetComponent<IHealth>().InitConfig(lvlStatsData.Hp);
            playerUnit.GetComponent<ITargetFinder>().InitConfig(targetData.TargetLayerMask);
            playerUnit.GetComponent<UnitAgentMover>().InitConfig(unitData.MoveSpeed, unitData.AttackDistance);

            var attacker = playerUnit.GetComponentInChildren<IAttacker>();

            if (attacker is MeleeAttacker meleeAttacker)
            {
                meleeAttacker.InitConfig(lvlStatsData.Damage, unitData.AttackCooldown,
                    targetData.TargetLayerMask);
            }
            else if (attacker is RangedAttacker rangedAttacker)
            {
                rangedAttacker.InitConfig(lvlStatsData.Damage, unitData.AttackCooldown,
                    targetData.TargetLayerMask, magicSphereData.Speed);
            }

            return playerUnit;
        }
    }
}