using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.DOTWeenAnimation
{
    public class ButtonPressAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _pressScale = 0.92f;
        [SerializeField] private float _pressDuration = 0.08f;
        [SerializeField] private Ease _pressEase = Ease.OutQuad;

        private Transform _transform;
        private Vector3 _originalScale;
        private Tween _pressTween;

        public event Action OnPressed;
        public event Action OnReleased;

        private void Awake()
        {
            _transform = transform;
            _originalScale = _transform.localScale;
        }

        private void OnDestroy()
            => _pressTween?.Kill();

        private void OnDisable()
        {
            _pressTween?.Kill();

            if (_transform != null)
                _transform.localScale = _originalScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPressed?.Invoke();
            _pressTween?.Kill();
            _pressTween = _transform.DOScale(_pressScale, _pressDuration).SetEase(_pressEase);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnReleased?.Invoke();
            _pressTween?.Kill();
            _pressTween = _transform.DOScale(_originalScale, _pressDuration).SetEase(_pressEase);
        }
    }
}