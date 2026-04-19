using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Settings
{
    public class OpenSettingsView : MonoBehaviour, IView
    {
        [SerializeField] private Button _spawnButton;

        public event Action OpenSettingsClicked;

        private void OnEnable()
            => _spawnButton.onClick.AddListener(OnOpenSettingsButtonClick);

        private void OnDisable()
            => _spawnButton.onClick.RemoveListener(OnOpenSettingsButtonClick);

        private void OnOpenSettingsButtonClick()
            => OpenSettingsClicked?.Invoke();
    }
}
