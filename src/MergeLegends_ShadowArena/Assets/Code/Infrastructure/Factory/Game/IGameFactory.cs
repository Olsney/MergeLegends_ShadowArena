using Code.Logic.PlayArea;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Factory.Game
{
    public interface IGameFactory
    {
        PlayZone CreatePlayZone();
        GameObject CreateMeleePlayerUnit(Vector3 position);
        GameObject CreateRangerPlayerUnit(Vector3 position);
        GameObject CreateMagicSpherePlayerUnit(Vector3 position);
        GameObject CreateEnemy(UnitTypeId unitTypeId, UnitLvlTypeId unitLvlTypeId, Vector3 position);
    }
}