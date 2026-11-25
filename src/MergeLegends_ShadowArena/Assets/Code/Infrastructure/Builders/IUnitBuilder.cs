using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Builders
{
    public interface IUnitBuilder
    {
        IUnitBuilder SetUnitType(UnitTypeId unitTypeId);
        IUnitBuilder SetUnitAttackType(UnitAttackTypeId unitAttackTypeId);
        IUnitBuilder SetCurrentLevel(int levelIndex);
        IUnitBuilder SetPosition(Vector3 position);
        GameObject Build();
    }
}