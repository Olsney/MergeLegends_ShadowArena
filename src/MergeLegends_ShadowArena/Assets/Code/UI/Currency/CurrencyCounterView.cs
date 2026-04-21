using Code.UI.Texts;
using UnityEngine;

namespace Code.UI.Currency
{
    public class CurrencyCounterView : MonoBehaviour, IView
    {
        private CurrencyText _currencyText;

        private void Awake()
            => _currencyText = GetComponentInChildren<CurrencyText>();

        public void UpdateCounter(int value) 
            => _currencyText.TextMeshPro.text = value.ToString();
    }
}