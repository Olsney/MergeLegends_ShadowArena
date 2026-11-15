using System;
using Code.UI.SpawnUnits.PriceUnits;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.SpawnUnits
{
    public class MeleePlayerUnitPurchaseView : MonoBehaviour, IView
    {
        [SerializeField] private Button _spawnButton;

        public event Action SpawnClicked;

        private MeleePriceText _priceText;

        private void Awake()
            => _priceText = GetComponentInChildren<MeleePriceText>();

        public void ChangeValue(long value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Price value cannot be negative");

            _priceText.TextMeshPro.text = value.ToString();
        }

        private void OnEnable()
            => _spawnButton.onClick.AddListener(OnSpawnButtonClick);

        private void OnDisable()
            => _spawnButton.onClick.RemoveListener(OnSpawnButtonClick);

        private void OnSpawnButtonClick()
            => SpawnClicked?.Invoke();

        public void Remove()
            => Destroy(gameObject);
    }
}