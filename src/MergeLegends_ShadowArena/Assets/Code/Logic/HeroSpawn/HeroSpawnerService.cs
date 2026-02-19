using System;
using Code.UI.Factory;
using UnityEngine;
using Zenject;

namespace Code.Logic.HeroSpawn
{
    public class HeroSpawnerService : IInitializable, IDisposable
    {
        private readonly IUIFactory _uiFactory;
        private readonly IHeroSpawner _heroSpawner;
        private HeroSpawnView _view;

        public HeroSpawnerService(IUIFactory uiFactory, IHeroSpawner heroSpawner)
        {
            _uiFactory = uiFactory;
            _heroSpawner = heroSpawner;
        }

        public void Initialize()
        {
            _view = _uiFactory.CreateSpawnHeroView().GetComponent<HeroSpawnView>();

            _uiFactory.UIRootCreated += InitHeroSpawnView;
            _view.SpawnClicked += _heroSpawner.Spawn;
        }

        public void Dispose()
        {
            _uiFactory.UIRootCreated -= InitHeroSpawnView;
            _view.SpawnClicked -= _heroSpawner.Spawn;
        }

        private void InitHeroSpawnView()
        {
            _view.transform.SetParent(_uiFactory.UiRoot.transform);
            _view.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}