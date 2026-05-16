using TMPro;
using UnityEngine;

namespace Code.UI.Rewards
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class RewardText : MonoBehaviour
    {
        public TextMeshProUGUI TextMeshPro { get; private set; }

        private void Awake()
            => TextMeshPro = GetComponent<TextMeshProUGUI>();
    }
}