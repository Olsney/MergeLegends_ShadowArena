using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Factory.Game
{
    public interface IGameFactory
    {
        GameObject CreatePlayZone(LocationTypeId locationTypeId);
        void CreateVictoryConfetti(LocationTypeId locationTypeId);
    }
}