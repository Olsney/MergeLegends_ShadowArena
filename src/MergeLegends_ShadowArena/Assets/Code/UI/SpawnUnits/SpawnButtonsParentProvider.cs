using UnityEngine;

namespace Code.UI.SpawnUnits
{
    public class SpawnButtonsParentProvider : ISpawnButtonsParentProvider
    {
        public Transform TransformParent { get; private set; }

        public void SetParent(Transform parent)
            => TransformParent = parent;
    }
}