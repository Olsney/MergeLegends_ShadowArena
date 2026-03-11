using Code.Logic.PlayArea;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Factory.Game
{
    public interface IGameFactory
    {
        PlayZone CreatePlayZone();
        GameObject CreateUnit(UnitTypeId unitTypeId, UnitAttackTypeId unitAttackTypeId,
            UnitLvlTypeId unitLvlTypeId, Vector3 position);
    }
}