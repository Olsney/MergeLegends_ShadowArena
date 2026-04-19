using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.DOTWeenAnimation
{
    public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _hoverScale = 1.05f;
        [SerializeField] private float _hoverDuration = 0.2f;
        [SerializeField] private Ease _hoverEase = Ease.OutQuad;

        private Transform _transform;
        private Vector3 _originalScale;
        private Tween _hoverTween;
        private bool _isHovering;

        private void Awake()
        {
            _transform = transform;
            _originalScale = _transform.localScale;
        }

        private void Update()
        {
            if (_isHovering && _transform.localScale == _originalScale)
            {
                _hoverTween?.Kill();
                _hoverTween = _transform.DOScale(_hoverScale, _hoverDuration).SetEase(_hoverEase);
            }
        }

        private void OnDestroy()
        {
            _hoverTween?.Kill();
        }

        private void OnDisable()
        {
            _hoverTween?.Kill();

            if (_transform != null)
                _transform.localScale = _originalScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovering = true;
            _hoverTween?.Kill();
            _hoverTween = _transform.DOScale(_hoverScale, _hoverDuration).SetEase(_hoverEase);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovering = false;
            _hoverTween?.Kill();
            _hoverTween = _transform.DOScale(_originalScale, _hoverDuration).SetEase(_hoverEase);
        }
    }
}