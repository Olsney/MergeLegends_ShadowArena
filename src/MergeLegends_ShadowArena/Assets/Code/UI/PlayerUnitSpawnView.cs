using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class PlayerUnitSpawnView : MonoBehaviour, IView
    {
        [SerializeField] private Button _spawnButton;

        public event Action SpawnClicked;

        public void Remove() 
            => Destroy(gameObject);

        private void OnEnable() 
            => _spawnButton.onClick.AddListener(OnSpawnButtonClick);

        private void OnDisable() 
            => _spawnButton.onClick.RemoveListener(OnSpawnButtonClick);

        private void OnSpawnButtonClick()
            => SpawnClicked?.Invoke();
    }
}