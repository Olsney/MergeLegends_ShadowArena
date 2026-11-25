using DG.Tweening;
using UnityEngine;

namespace Code.UI.DOTWeenAnimation
{
    public class ButtonIdleAnimation : MonoBehaviour
    {
        [SerializeField] private float _idleScale = 1.03f;
        [SerializeField] private float _idleDuration = 1.2f;
        [SerializeField] private bool _isActiveToStart = false;

        private Transform _transform;
        private Tween _pressTween;

        private void Start()
        {
            _transform = transform;
            
            if (_isActiveToStart)
                StartAnimation();
        }

        public void StartAnimation()
        {
            _pressTween?.Kill();
            _pressTween = _transform.DOScale(_idleScale, _idleDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}