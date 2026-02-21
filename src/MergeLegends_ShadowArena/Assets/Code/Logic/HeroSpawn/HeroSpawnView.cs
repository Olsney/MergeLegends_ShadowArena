using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Logic.HeroSpawn
{
    public class HeroSpawnView : MonoBehaviour, IHeroSpawnView
    {
        [SerializeField] private Button _spawnButton;

        public event Action SpawnClicked;

        private void OnEnable() => _spawnButton.onClick.AddListener(() => SpawnClicked?.Invoke());

        private void OnDisable() => _spawnButton.onClick.AddListener(() => SpawnClicked?.Invoke());
    }
}