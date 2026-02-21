using System;

namespace Code.Logic.HeroSpawn
{
    public class HeroSpawnerService : IDisposable, IHeroSpawnerService
    {
        private readonly IHeroSpawner _heroSpawner;
        private IHeroSpawnView _view;

        public HeroSpawnerService(IHeroSpawner heroSpawner)
        {
            _heroSpawner = heroSpawner;
        }

        public void InitializeView(IHeroSpawnView view)
        {
            _view = view;
            _view.SpawnClicked += _heroSpawner.Spawn;
        }

        public void Dispose()
        {
            _view.SpawnClicked -= _heroSpawner.Spawn;
        }
    }
}