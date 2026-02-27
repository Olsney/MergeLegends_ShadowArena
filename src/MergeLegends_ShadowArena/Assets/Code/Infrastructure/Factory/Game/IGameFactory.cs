using UnityEngine;

namespace Code.Infrastructure.Factory.Game
{
    public interface IGameFactory
    {
        GameObject CreatePlayZone();
        GameObject CreatePlayerUnit(Vector3 position);
    }
}