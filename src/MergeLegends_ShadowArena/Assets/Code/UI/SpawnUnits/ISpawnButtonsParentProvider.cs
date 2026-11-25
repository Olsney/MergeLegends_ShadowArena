using UnityEngine;

namespace Code.UI.SpawnUnits
{
    public interface ISpawnButtonsParentProvider
    {
        Transform TransformParent { get; }
        void SetParent(Transform parent);
    }
}