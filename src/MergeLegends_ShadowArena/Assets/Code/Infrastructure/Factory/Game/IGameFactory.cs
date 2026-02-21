using UnityEngine;

namespace Code.Infrastructure.Factory.Game
{
    public interface IGameFactory
    {
        GameObject CreatePlayZone(Vector3 playerUnitGrdGridZone, Vector3 enemiesGridZone);
        GameObject CreatePlayerUnit(Transform parent);
    }
}