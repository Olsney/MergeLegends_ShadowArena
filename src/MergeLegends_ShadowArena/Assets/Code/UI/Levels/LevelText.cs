using TMPro;
using UnityEngine;

namespace Code.UI.Texts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LevelText : MonoBehaviour
    {
        public TextMeshProUGUI TextMeshPro { get; private set; }

        private void Awake()
            => TextMeshPro = GetComponent<TextMeshProUGUI>();
    }
}