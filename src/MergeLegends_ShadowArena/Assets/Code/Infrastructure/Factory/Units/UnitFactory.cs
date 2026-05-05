using System;
using Code.Services.StaticData;
using Code.StaticData;
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
            UnitStaticData unitStaticData = GetUnitStaticData(unitTypeId, unitAttackTypeId);
    
            if (unitStaticData.UnitStatsByLevelIndexSetups == null || unitStaticData.UnitStatsByLevelIndexSetups.Count == 0)
                throw new InvalidOperationException($"UnitStatsByLevelIndexSetups is empty for {unitStaticData.name}");
    
            if (unitCurrentLevelIndex < 0 || unitCurrentLevelIndex >= unitStaticData.UnitStatsByLevelIndexSetups.Count)
                throw new ArgumentOutOfRangeException(nameof(unitCurrentLevelIndex), 
                    $"Level {unitCurrentLevelIndex} not found in {unitStaticData.name}");

            UnitLevelStatsData stats = unitStaticData.UnitStatsByLevelIndexSetups[unitCurrentLevelIndex].UnitLevelStatsData;
    
            Quaternion rotation = unitTypeId == UnitTypeId.Enemy
                ? Quaternion.Euler(0, EnemySpawnRotationY, 0)
                : Quaternion.identity;

            return _instantiator.InstantiatePrefab(stats.ViewPrefab, position, rotation, null);
        }

        private UnitStaticData GetUnitStaticData(UnitTypeId unitTypeId, UnitAttackTypeId unitAttackTypeId)
        {
            switch (unitTypeId)
            {
                case UnitTypeId.PlayerUnit:
                    return _staticData.GetPlayerUnit(unitAttackTypeId);
                case UnitTypeId.Enemy:
                    return _staticData.GetEnemy(unitAttackTypeId);
                default:
                    throw new ArgumentOutOfRangeException(nameof(unitTypeId), unitTypeId, null);
            }
        }
    }
}