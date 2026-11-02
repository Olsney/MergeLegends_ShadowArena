using DG.Tweening;
using UnityEngine;

namespace Code.UI.Currency
{
    public class CurrencyCounterView : MonoBehaviour, IView
    {
        [SerializeField] private float _animationAddedDuration = 1.5f;

        private Transform _transform;
        private Tween _tween;

        private CurrencyText _currencyText;
        private Transform _currencyTextTransform;

        private long _startValue;

        private void Awake()
        {
            _transform = transform;
            _currencyText = GetComponentInChildren<CurrencyText>();
            _currencyTextTransform = _currencyText.transform;
        }

        private void OnDestroy()
        {
            _tween?.Kill();

            if (_transform != null)
                DOTween.Kill(_transform);

            DOTween.Kill(gameObject);
        }

        public void UpdateCounter(long endValue)
        {
            _transform.SetAsLastSibling();
            StartIncreaseAnimate(endValue);
        }

        private void StartIncreaseAnimate(long endValue)
        {
            if (this == null || gameObject == null)
                return;

            _tween?.Kill();

            long currentValue = _startValue;

            _tween = DOTween.To(() => currentValue, x =>
                {
                    currentValue = Mathf.RoundToInt(x);

                    if (_currencyText != null && _currencyText.TextMeshPro != null)
                        _currencyText.TextMeshPro.text = currentValue.ToString();
                    
                }, endValue, _animationAddedDuration)
                .SetEase(Ease.OutQuad)
                .OnUpdate(() =>
                {
                    if (_currencyTextTransform == null)
                        return;

                    float scale = 1f + Mathf.Sin(Time.time * 30f) * 0.1f;
                    _currencyTextTransform.localScale = Vector3.one * scale;
                })
                .OnComplete(() =>
                {
                    if (_currencyTextTransform != null)
                        _currencyTextTransform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 2, 0.5f);

                    _startValue = endValue;
                });
        }
    }
}