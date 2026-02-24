using System;
using Code.Logic.ViewProvider;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Logic.PlayerUnitSpawn
{
    public class PlayerUnitSpawnView : MonoBehaviour, IPlayerUnitSpawnView
    {
        [SerializeField] private Button _spawnButton;

        private IViewProvider _viewProvider;

        public event Action SpawnClicked;

        private void Awake()
            => _viewProvider.AddView(this);

        [Inject]
        public void Construct(IViewProvider viewProvider)
            => _viewProvider = viewProvider;

        public void Show()
            => gameObject.SetActive(true);

        public void Hide()
            => gameObject.SetActive(false);

        private void OnEnable() => _spawnButton.onClick.AddListener(OnSpawnButtonClick);

        private void OnDisable() => _spawnButton.onClick.RemoveListener(OnSpawnButtonClick);

        private void OnSpawnButtonClick()
            => SpawnClicked?.Invoke();
    }
}