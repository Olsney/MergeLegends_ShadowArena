using UnityEngine;
using UnityEngine.UI;

namespace Code.Units.UI.Health
{
    public class HealthFollowSlider : MonoBehaviour
    {
        private Slider _slider;

        public Slider Slider => _slider;

        private void Awake()
            => _slider = GetComponent<Slider>();
    }
}