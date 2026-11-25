using UnityEngine;
using Code.StaticData;
using Code.Units.MagicSphereAttack;

namespace Code.Infrastructure.Factory.Projectile
{
    public interface IProjectileFactory
    {
        MagicSphere CreateMagicSphere(UnitTypeId unitTypeId, Vector3 position);
    }
}