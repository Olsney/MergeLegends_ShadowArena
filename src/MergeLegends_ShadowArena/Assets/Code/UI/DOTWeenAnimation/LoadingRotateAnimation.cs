using DG.Tweening;
using UnityEngine;

namespace Code.UI.DOTWeenAnimation
{
    public class LoadingRotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration = 1f;

        private const float RotationAngle = 360f;

        private RectTransform _rectTransform;
        private Tween _tween;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (_rectTransform == null)
                Debug.LogError("Missing RectTransform");
        }

        private void OnEnable()
            => StartAnimation();

        private void OnDisable()
            => StopAnimation();

        private void StartAnimation()
        {
            StopAnimation();

            _tween = _rectTransform
                .DORotate(new Vector3(0, 0, RotationAngle), _duration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear)
                .SetUpdate(true);
        }

        private void StopAnimation()
        {
            if (_tween != null && _tween.IsActive())
            {
                _tween.Kill();
                _tween = null;
            }
        }
    }
}