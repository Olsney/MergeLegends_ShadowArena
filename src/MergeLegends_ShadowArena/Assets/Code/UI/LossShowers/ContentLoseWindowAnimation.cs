using Code.UI.DOTWeenAnimation;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Code.UI.LossShowers
{
    public class ContentLoseWindowAnimation : MonoBehaviour
    {
        private Transform _transform;
        private Tween _tween;
        private Vector3 _originalScale;

        private void Awake()
        {
            _transform = transform;
            _originalScale = _transform.localScale;
        }

        private void OnDestroy()
        {
            _tween?.Kill();

            if (_transform != null)
                DOTween.Kill(_transform);

            DOTween.Kill(gameObject);
        }

        public async UniTask StartAnimationAsync()
        {
            if (this == null || gameObject == null)
                return;

            _transform.localScale = Vector3.zero;

            if (this == null || gameObject == null)
                return;

            await _transform.DOScale(_originalScale, 0.6f).SetEase(Ease.OutCubic).AsyncWaitForCompletion();
        }
    }
}