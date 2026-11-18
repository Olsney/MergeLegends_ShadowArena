using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.TeamHealth
{
    public class TeamHealthBarView : MonoBehaviour, IView
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _animationDuration;

        private Tween _tween;

        private void OnDestroy()
            => _tween?.Kill();

        public void SetHealth(float normalizedValue)
        {
            _tween?.Kill();
            _slider.value = Mathf.Clamp01(normalizedValue);
        }

        public void UpdateHealth(float normalizedValue)
        {
            _tween?.Kill();
            _tween = _slider.DOValue(Mathf.Clamp01(normalizedValue), _animationDuration)
                .SetEase(Ease.OutQuad)
                .SetLink(gameObject);
        }
    }
}