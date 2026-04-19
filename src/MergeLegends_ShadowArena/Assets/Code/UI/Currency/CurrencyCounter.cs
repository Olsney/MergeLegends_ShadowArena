using Code.Services.PersistentProgress;
using Code.UI.Texts;
using UnityEngine;
using Zenject;

namespace Code.UI.Currency
{
    public class CurrencyCounter : MonoBehaviour
    {
        private CurrencyText currencyText;
        private IPersistentProgressService _progressService;

        private void Awake()
            => currencyText = GetComponentInChildren<CurrencyText>();

        private void Start()
            => UpdateCounter();

        private void OnDestroy()
            => _progressService.Progress.WorldData.CurrencyData.Changed -= UpdateCounter;

        [Inject]
        public void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _progressService.Progress.WorldData.CurrencyData.Changed += UpdateCounter;
        }

        private void UpdateCounter()
            => currencyText.TextMeshPro.text = $"{_progressService.Progress.WorldData.CurrencyData.Value}";
    }
}