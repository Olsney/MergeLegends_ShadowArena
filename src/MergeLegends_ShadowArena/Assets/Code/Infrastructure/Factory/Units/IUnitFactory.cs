using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factory.Units
{
    public interface IUnitFactory
    {
        GameObject CreateUnit(UnitTypeId unitTypeId, UnitAttackTypeId unitAttackTypeId, int unitCurrentLevelIndex,
            Vector3 position);
        void SetSceneContainer(DiContainer sceneContainer);
    }
}