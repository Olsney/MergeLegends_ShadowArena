using UnityEngine;

namespace Code.UI.Tutorial
{
    public static class SpotlightTargetProjector
    {
        public static Vector2 ProjectUI(RectTransform target, Camera canvasCamera) =>
            RectTransformUtility.WorldToScreenPoint(
                canvasCamera,
                target.TransformPoint(target.rect.center));

        public static bool TryProjectWorld(Transform target, Camera worldCamera, out Vector2 screenPoint)
        {
            Vector3 point = worldCamera.WorldToScreenPoint(target.position);
            screenPoint = point;

            return point.z >= 0f;
        }
    }
}