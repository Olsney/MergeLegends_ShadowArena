using TMPro;
using UnityEngine;

namespace Code.UI.WarningMessages
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class WarningMessageText : MonoBehaviour
    {
        public TextMeshProUGUI TextMeshPro { get; private set; }

        private void Awake()
            => TextMeshPro = GetComponent<TextMeshProUGUI>();
    }
}