using UnityEngine;

namespace Code.Infrastructure.Factory.Game
{
    public interface IGameFactory
    {
        GameObject CreatePlayZone();
        GameObject CreateMeleePlayerUnit(Vector3 position);
        GameObject CreateRangerPlayerUnit(Vector3 position);
        GameObject CreateMagicSpherePlayerUnit(Vector3 position);
    }
}