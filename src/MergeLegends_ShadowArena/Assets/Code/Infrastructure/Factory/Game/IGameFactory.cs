using UnityEngine;

namespace Code.Infrastructure.Factory.Game
{
    public interface IGameFactory
    {
        GameObject CreatePlayZone(Vector3 heroesGridZone, Vector3 enemiesGridZone);
        GameObject CreateHero(Transform parent);
    }
}