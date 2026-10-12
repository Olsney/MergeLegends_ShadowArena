using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class LocationSceneEntryData
    {
        public LocationTypeId Type;

#if UNITY_EDITOR
        public UnityEditor.SceneAsset Scene;
#endif

        [HideInInspector] public string SceneName;
        
        public Vector3 PlayerUnitsGridPlacesPosition;
        public Vector3 EnemiesGridPlacesPosition;
        public List<Vector3> ConfettiEffectSpawnPointPosition;
    }
}