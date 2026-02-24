using System;
using Code.UI.View;

namespace Code.Logic.PlayerUnitSpawn
{
    public interface IPlayerUnitSpawnView : IView
    {
        event Action SpawnClicked;
    }
}