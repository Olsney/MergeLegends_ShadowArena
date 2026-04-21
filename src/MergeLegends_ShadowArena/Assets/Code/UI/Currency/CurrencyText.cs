using TMPro;
using UnityEngine;

namespace Code.UI.Currency
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CurrencyText : MonoBehaviour
    {
        public TextMeshProUGUI TextMeshPro { get; private set; }

        private void Awake()
            => TextMeshPro = GetComponent<TextMeshProUGUI>();
    }
}