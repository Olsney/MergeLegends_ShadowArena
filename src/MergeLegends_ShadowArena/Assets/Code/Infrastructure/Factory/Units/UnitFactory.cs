using System;
using System.Collections.Generic;
using Code.Services.SceneProvider;
using Code.Services.StaticData;
using Code.Services.UnitStatsServices;
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
        private readonly ISceneProvider _sceneProvider;
        private readonly IUnitStatsService _unitStatsService;

        public UnitFactory(
            IInstantiator instantiator,
            IStaticDataService staticData,
            ISceneProvider sceneProvider,
            IUnitStatsService unitStatsService)
        {
            _instantiator = instantiator;
            _staticData = staticData;
            _sceneProvider = sceneProvider;
            _unitStatsService = unitStatsService;
        }

        public GameObject CreateUnit(UnitTypeId unitTypeId, UnitAttackTypeId unitAttackTypeId,
            int unitCurrentLevelIndex, Vector3 position)
        {
            UnitStaticData unitStaticData = GetUnitStaticData(unitTypeId, unitAttackTypeId);
            GameObject unitPrefab = _unitStatsService.GetStats(unitStaticData, unitCurrentLevelIndex).ViewPrefab;

            Quaternion rotation = unitTypeId == UnitTypeId.Enemy
                ? Quaternion.Euler(0, EnemySpawnRotationY, 0)
                : Quaternion.identity;

            return _instantiator.InstantiatePrefab(unitPrefab, position, rotation, _sceneProvider.Container);
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