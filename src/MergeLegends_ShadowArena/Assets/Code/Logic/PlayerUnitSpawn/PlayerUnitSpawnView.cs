using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Logic.PlayerUnitSpawn
{
    public class PlayerUnitSpawnView : MonoBehaviour, IPlayerUnitSpawnView
    {
        [SerializeField] private Button _spawnButton;

        public event Action SpawnClicked;

        private void OnEnable() => _spawnButton.onClick.AddListener(OnSpawnButtonClick);

        private void OnDisable() => _spawnButton.onClick.RemoveListener(OnSpawnButtonClick);

        private void OnSpawnButtonClick()
            => SpawnClicked?.Invoke();
    }
}