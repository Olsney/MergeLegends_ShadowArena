using DG.Tweening;
using UnityEngine;

namespace Code.UI.DOTWeenAnimation
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ButtonAppearAnimation : MonoBehaviour
    {
        [SerializeField] private float _appearDuration = 3f;
        [SerializeField] private float _activeThreshold = 0.5f;
        [SerializeField] private Ease _appearEase = Ease.OutQuad;

        private Transform _transform;
        private Tween _tween;
        private CanvasGroup _canvasGroup;
        private bool _isAnimated;

        private void OnDestroy()
        {
            _tween?.Kill();

            if (_transform != null)
                DOTween.Kill(_transform);

            DOTween.Kill(gameObject);
        }

        private void Awake()
        {
            _transform = transform;
            _canvasGroup = GetComponent<CanvasGroup>();

            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void StartAnimation()
        {
            if (_isAnimated) return;
            _isAnimated = true;

            _tween = _canvasGroup.DOFade(1f, _appearDuration)
                .SetEase(_appearEase)
                .OnUpdate(() =>
                {
                    if (_canvasGroup.alpha >= _activeThreshold && !_canvasGroup.interactable)
                    {
                        _canvasGroup.interactable = true;
                        _canvasGroup.blocksRaycasts = true;
                    }
                });
        }
    }
}