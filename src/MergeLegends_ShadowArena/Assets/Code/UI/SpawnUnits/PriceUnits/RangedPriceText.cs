using TMPro;
using UnityEngine;

namespace Code.UI.SpawnUnits.PriceUnits
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class RangedPriceText : MonoBehaviour
    {
        public TextMeshProUGUI TextMeshPro { get; private set; }

        private void Awake()
            => TextMeshPro = GetComponent<TextMeshProUGUI>();
    }
}