using UnityEngine;
using UnityEngine.UI;

namespace Code.Units.UI.Health
{
    public class HealthMainSlider : MonoBehaviour
    {
        private Slider _slider;

        public Slider Slider => _slider;

        private void Awake()
            => _slider = GetComponent<Slider>();
    }
}