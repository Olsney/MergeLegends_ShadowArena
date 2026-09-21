using Code.UI.Factory;
using Code.UI.Tutorial;
using UnityEngine;

namespace Code.Services.Tutorial
{
    public class TutorialPointerService : ITutorialPointerService
    {
        private static readonly Quaternion RightRotation = Quaternion.Euler(0f, 180f, 0f);

        private readonly IUIFactory _uiFactory;

        private TutorialPointerAnimation _pointerAnimation;

        public TutorialPointerService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Init()
        {
            _pointerAnimation = _uiFactory.CreateTutorialPoint().GetComponent<TutorialPointerAnimation>();
            _pointerAnimation.gameObject.SetActive(false);
        }

        public void Show(RectTransform space, Vector2 localCenter, float radius, TutorialPointerSide side)
        {
            if (_pointerAnimation == null)
                Init();

            localCenter.x += side.ToDirection() * radius;

            _pointerAnimation.transform.localRotation =
                side == TutorialPointerSide.Left
                    ? Quaternion.identity
                    : RightRotation;

            _pointerAnimation.gameObject.SetActive(true);
            _pointerAnimation.Play(space.TransformPoint(localCenter), side);
        }

        public void Hide()
        {
            if (_pointerAnimation == null)
                return;

            _pointerAnimation.gameObject.SetActive(false);
        }
    }
}