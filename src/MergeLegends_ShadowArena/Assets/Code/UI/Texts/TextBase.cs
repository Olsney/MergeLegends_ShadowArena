using TMPro;
using UnityEngine;

namespace Code.UI.Texts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextBase : MonoBehaviour
    {
        public TextMeshProUGUI TextMeshPro { get; private set; }

        private void Awake()
            => TextMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }
}