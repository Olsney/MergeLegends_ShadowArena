using UnityEngine;

namespace Code.Services.SceneProvider
{
    public interface ISceneProvider
    {
        Transform Container { get; }
        void SetTransform(Transform transform);
    }
}