using System;
using Code.Services.SceneProvider;
using Code.Services.StaticData;
using Code.StaticData;
using Code.Units.MagicSphereAttack;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory.Projectile
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IStaticDataService _staticData;
        private readonly ISceneProvider _sceneProvider;

        private GameObject _playZone;

        public ProjectileFactory(IInstantiator instantiator, IStaticDataService staticData,
            ISceneProvider sceneProvider)
        {
            _instantiator = instantiator;
            _staticData = staticData;
            _sceneProvider = sceneProvider;
        }

        public MagicSphere CreateMagicSphere(UnitTypeId unitTypeId, Vector3 position)
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

            return _instantiator.InstantiatePrefab(
                    unitStaticData.GeneralData.MagicSphereData.ViewPrefab,
                    position,
                    Quaternion.identity,
                    _sceneProvider.Container)
                .GetComponent<MagicSphere>();
        }
    }
}