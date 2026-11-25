using TMPro;
using UnityEngine;

namespace Code.Units.UI.Texts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UnitLevelText : MonoBehaviour
    {
        public TextMeshProUGUI TextMeshPro { get; private set; }

        private void Awake()
            => TextMeshPro = GetComponent<TextMeshProUGUI>();
    }
}