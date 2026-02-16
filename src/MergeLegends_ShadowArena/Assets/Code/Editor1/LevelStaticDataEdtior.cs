using Code.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor1
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEdtior : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.HeroesPlacesMarkerPosition = FindAnyObjectByType<HeroesPlacesMarker>().transform.position;
                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }
        
            EditorUtility.SetDirty(target);
        }
    }
}