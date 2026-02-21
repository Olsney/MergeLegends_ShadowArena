using System;

namespace Code.Logic.HeroSpawn
{
    public interface IHeroSpawnView
    {
        event Action SpawnClicked;
    }
}