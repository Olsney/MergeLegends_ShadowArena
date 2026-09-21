using UnityEngine;

namespace Code.Services.Tutorial
{
    public interface ITutorialPointerService
    {
        void Init();
        void Show(RectTransform space, Vector2 localCenter, float radius, TutorialPointerSide side);
        void Hide();
    }
}