using Code.Logic.PlayArea;

namespace Code.Infrastructure.Factory.Game
{
    public interface IGameFactory
    {
        PlayZone CreatePlayZone();
    }
}