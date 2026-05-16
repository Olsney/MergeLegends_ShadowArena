using Code.Logic.PlayArea;
using Zenject;

namespace Code.Infrastructure.Factory.Game
{
    public interface IGameFactory
    {
        PlayZone CreatePlayZone();
        void CreateVictoryConfetti();
        void SetSceneContainer(DiContainer sceneContainer);
    }
}