using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Factory.Units
{
    public interface IUnitFactory
    {
        GameObject CreateUnit(UnitTypeId unitTypeId, UnitAttackTypeId unitAttackTypeId, int unitCurrentLevelIndex,
            Vector3 position);
    }
}