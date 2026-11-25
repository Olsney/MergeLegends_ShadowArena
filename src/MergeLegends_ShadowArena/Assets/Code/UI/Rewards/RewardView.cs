using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Code.UI.Rewards
{
    public class RewardView : MonoBehaviour
    {
        [SerializeField] private float _duration = 1.2f;
        [SerializeField] private float _maxScale = 1.2f;

        private RewardText _rewardText;
        private Vector3 _originalScale;
        private Transform _transform;
        private Tween _tween;
        
        private long _rewardValue;

        private void Awake()
        {
            _transform = transform;
            _rewardText = GetComponentInChildren<RewardText>();
            _originalScale = _transform.localScale;
        }

        private void OnDestroy()
        {
            _tween?.Kill();

            if (_transform != null)
                DOTween.Kill(_transform);

            DOTween.Kill(gameObject);
        }

        public void SetValue(long rewardValue)
        {
            _rewardValue = rewardValue;
            _rewardText.TextMeshPro.text = rewardValue.ToString();
        }

        public async UniTask StartAnimationAsync()
        {
            if (this == null || gameObject == null)
                return;

            _rewardText.TextMeshPro.text = _rewardValue.ToString();

            long startValue = _rewardValue;
            int endValue = 0;
            long currentValue = startValue;

            await DOTween.To(() => currentValue, x =>
                {
                    currentValue = Mathf.RoundToInt(x);

                    if (_rewardText != null && _rewardText.TextMeshPro != null)
                        _rewardText.TextMeshPro.text = currentValue.ToString();
                }, endValue, _duration)
                .SetEase(Ease.InQuad)
                .OnUpdate(() =>
                {
                    if (this == null || gameObject == null || _transform == null)
                        return;

                    float t = 1f - (float)currentValue / startValue;
                    float scale = _originalScale.x + (_maxScale - _originalScale.x) * t;
                    _transform.localScale = Vector3.one * scale;
                })
                .AsyncWaitForCompletion();

            if (this != null && gameObject != null && _transform != null)
                await _transform.DOScale(_originalScale, 0.2f).AsyncWaitForCompletion();
        }
    }
}