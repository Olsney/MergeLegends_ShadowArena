using System;
using Code.Services.StaticData;
using Code.StaticData;
using Code.Units;
using Code.Units.Attack;
using Code.Units.UI;
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
                        unitTypeId);
                case UnitTypeId.Enemy:
                    return CreateConcreteUnit(
                        _staticData.GetEnemy(unitAttackTypeId),
                        unitCurrentLevelIndex,
                        position,
                        Quaternion.Euler(0, EnemySpawnRotationY, 0),
                        unitTypeId);
                default:
                    throw new ArgumentOutOfRangeException(nameof(unitTypeId), unitTypeId, null);
            }
        }

        private GameObject CreateConcreteUnit(UnitStaticData unitStaticData, int unitCurrentLevelIndex, 
            Vector3 position, Quaternion rotation, UnitTypeId unitTypeId)
        {
            UnitLevelStatsData stats = unitStaticData.GetStatsLvl(unitCurrentLevelIndex);
            TargetData targetData = unitStaticData.GeneralData.TargetData;
            UnitData unitData = unitStaticData.GeneralData.UnitData;
            MagicSphereData magicData = unitStaticData.GeneralData.MagicSphereData;

            GameObject unit = _instantiator.InstantiatePrefab(stats.ViewPrefab, position, rotation, null);

            InitServices(unit, stats, targetData, unitData, magicData, unitTypeId);
            CreateUnitLevelPresenter(unit, unitCurrentLevelIndex);

            return unit;
        }

        private void InitServices(GameObject unit, UnitLevelStatsData stats, TargetData targetData, UnitData unitData,
            MagicSphereData magicData, UnitTypeId unitTypeId)
        {
            unit.GetComponent<IHealth>().Init(stats.Hp);
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
        }

        private void CreateUnitLevelPresenter(GameObject unit, int unitCurrentLevelIndex)
        {
            var presenter =
                new UnitLevelPresenter(unit.GetComponentInChildren<UnitLevelView>(), unitCurrentLevelIndex);
            presenter.Initialize();
        }
    }
}