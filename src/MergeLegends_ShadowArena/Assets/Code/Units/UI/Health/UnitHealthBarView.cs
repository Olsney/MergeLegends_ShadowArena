using Code.UI;
using DG.Tweening;
using UnityEngine;

namespace Code.Units.UI.Health
{
    public class UnitHealthBarView : MonoBehaviour, IView
    {
        [SerializeField] private float _animationDuration = 0.2f;
        [SerializeField] private float _delayDuration = 0.3f;

        private HealthMainSlider _healthMainSlider;
        private HealthFollowSlider _healthFollowSlider;
        private Tween _currentTween;
        private Tween _secondTween;

        private void Awake()
        {
            _healthMainSlider = GetComponentInChildren<HealthMainSlider>();
            _healthFollowSlider = GetComponentInChildren<HealthFollowSlider>();
        }

        private void OnDestroy()
        {
            _currentTween?.Kill();
            _secondTween?.Kill();
        }

        public void Show()
            => gameObject.SetActive(true);

        public void Hide()
            => gameObject.SetActive(false);

        public void UpdateHealth(float normalizedValue)
        {
            normalizedValue = Mathf.Clamp01(normalizedValue);

            _currentTween?.Kill();
            _currentTween = _healthMainSlider.Slider.DOValue(normalizedValue, _animationDuration)
                .SetEase(Ease.OutQuad);

            _secondTween?.Kill();
            _secondTween = _healthFollowSlider.Slider.DOValue(normalizedValue, _animationDuration)
                .SetDelay(_delayDuration)
                .SetEase(Ease.OutQuad);
        }
    }
}