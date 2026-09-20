using Code.Services.StaticData;
using Code.Services.Tutorial;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.UI.Tutorial
{
    public class TutorialPointerAnimation : MonoBehaviour
    {
        private IStaticDataService _staticDataService;

        private RectTransform _rectTransform;
        private Tween _tween;
        private float _moveDuration;
        private float _moveDistance;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;

            TutorialStaticData config = _staticDataService.GetTutorialData();

            _moveDuration = config.PointerMoveDuration;
            _moveDistance = config.PointerMoveDistance;
        }

        private void OnDisable() =>
            ResetAnimation();

        public void Play(Vector3 worldPosition, TutorialPointerSide side)
        {
            _tween?.Kill();

            _rectTransform.position = worldPosition;

            Vector2 offset = new(side.ToDirection() * _moveDistance, _moveDistance);

            _tween = _rectTransform
                .DOAnchorPos(_rectTransform.anchoredPosition + offset, _moveDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void ResetAnimation()
        {
            _tween?.Kill();
            _tween = null;
        }
    }
}