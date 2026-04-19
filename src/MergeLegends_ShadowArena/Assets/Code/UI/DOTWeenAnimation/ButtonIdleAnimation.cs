using DG.Tweening;
using UnityEngine;

namespace Code.UI.DOTWeenAnimation
{
    public class ButtonIdleAnimation : MonoBehaviour
    {
        [SerializeField] private float _idleScale = 1.03f;
        [SerializeField] private float _idleDuration = 1.2f;

        private Transform _transform;
        private Vector3 _originalScale;
        private Tween _idleTween;

        private void Awake()
        {
            _transform = transform;
            _originalScale = _transform.localScale;
        }

        private void OnEnable()
        {
            StartIdle();
        }

        private void OnDisable()
        {
            _idleTween?.Kill();

            if (_transform != null)
                _transform.localScale = _originalScale;
        }

        private void OnDestroy()
        {
            _idleTween?.Kill();
        }

        private void StartIdle()
        {
            _idleTween = _transform.DOScale(_idleScale, _idleDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}