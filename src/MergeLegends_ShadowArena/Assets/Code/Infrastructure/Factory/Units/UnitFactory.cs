using System;
using Code.Services.StaticData;
using Code.StaticData;
using Code.Units;
using Code.Units.Attack;
using Code.Units.PlayerUnits;
using Code.Units.UI.Health;
using Code.Units.UI.Levels;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory.Units
{
    public class UnitFactory : IUnitFactory
    {
        private const float EnemySpawnRotationY = 180f;

        private readonly IInstantiator _instantiator;
        private readonly IStaticDataService _staticData;

        public UnitFactory(
            IInstantiator instantiator,
            IStaticDataService staticData)
        {
            _instantiator = instantiator;
            _staticData = staticData;
        }

        public GameObject CreateUnit(UnitTypeId unitTypeId, UnitAttackTypeId unitAttackTypeId,
            int unitCurrentLevelIndex, Vector3 position)
        {
            switch (unitTypeId)
            {
                case UnitTypeId.PlayerUnit:
                    return CreateConcreteUnit(
                        _staticData.GetPlayerUnit(unitAttackTypeId),
                        unitCurrentLevelIndex,
                        position,
                        Quaternion.identity,
                        unitTypeId,
                        unitAttackTypeId);
                case UnitTypeId.Enemy:
                    return CreateConcreteUnit(
                        _staticData.GetEnemy(unitAttackTypeId),
                        unitCurrentLevelIndex,
                        position,
                        Quaternion.Euler(0, EnemySpawnRotationY, 0),
                        unitTypeId,
                        unitAttackTypeId);
                default:
                    throw new ArgumentOutOfRangeException(nameof(unitTypeId), unitTypeId, null);
            }
        }

        private GameObject CreateConcreteUnit(UnitStaticData unitStaticData, int unitCurrentLevelIndex,
            Vector3 position, Quaternion rotation, UnitTypeId unitTypeId, UnitAttackTypeId unitAttackTypeId)
        {
            UnitLevelStatsData stats = unitStaticData.GetStatsLvl(unitCurrentLevelIndex);
            TargetData targetData = unitStaticData.GeneralData.TargetData;
            UnitData unitData = unitStaticData.GeneralData.UnitData;
            MagicSphereData magicData = unitStaticData.GeneralData.MagicSphereData;

            GameObject unit = _instantiator.InstantiatePrefab(stats.ViewPrefab, position, rotation, null);

            InitServices(unit, stats, targetData, unitData, magicData, unitTypeId, unitCurrentLevelIndex,
                unitAttackTypeId);

            return unit;
        }

        private void InitServices(GameObject unit, UnitLevelStatsData stats, TargetData targetData, UnitData unitData,
            MagicSphereData magicData, UnitTypeId unitTypeId, int unitCurrentLevelIndex,
            UnitAttackTypeId unitAttackTypeId)
        {
            if (unit.TryGetComponent<PlayerUnit>(out var playerUnit))
                playerUnit.Init(unitCurrentLevelIndex, unitAttackTypeId);

            IHealth health = unit.GetComponent<IHealth>();
            health.Init(stats.Hp);
            unit.GetComponent<ITargetFinder>().Init(targetData.TargetLayerMask);
            unit.GetComponent<UnitNavmeshAgentMover>().InitConfig(unitData.MoveSpeed, unitData.AttackDistance);

            switch (unit.GetComponentInChildren<IAttacker>())
            {
                case MeleeAttacker melee:
                    melee.Init(
                        damage: stats.Damage,
                        cooldown: unitData.AttackCooldown,
                        targetLayerMask: targetData.TargetLayerMask);
                    break;
                case RangedAttacker ranged:
                    ranged.Init(
                        damage: stats.Damage,
                        cooldown: unitData.AttackCooldown,
                        targetLayerMask: targetData.TargetLayerMask,
                        magicSphereSpeed: magicData.Speed,
                        unitTypeId: unitTypeId);
                    break;
                default:
                    throw new NullReferenceException("Unknown attacker type");
            }

            CreateUnitLevelPresenter(unit.GetComponentInChildren<UnitLevelView>(), unitCurrentLevelIndex);
            CreateUnitHealthBarPresenter(unit.GetComponentInChildren<UnitHealthBarView>(), health);
        }

        private void CreateUnitHealthBarPresenter(UnitHealthBarView view, IHealth health)
        {
            CreatePresenter(view, healthBarView =>
                new UnitHealthBarPresenter(healthBarView, health));
        }

        private void CreateUnitLevelPresenter(UnitLevelView view, int unitCurrentLevelIndex)
        {
            CreatePresenter(view, levelView =>
                new UnitLevelPresenter(levelView, unitCurrentLevelIndex));
        }

        private void CreatePresenter<TView, TPresenter>(
            TView view,
            Func<TView, TPresenter> createPresenter)
            where TView : MonoBehaviour
            where TPresenter : IInitializable
        {
            TPresenter presenter = createPresenter(view);
            presenter.Initialize();
        }
    }
}