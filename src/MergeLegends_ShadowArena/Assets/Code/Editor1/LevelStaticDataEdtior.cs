using Code.Logic.PlayArea;
using Code.Logic.PlayArea.Grid;
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
                levelData.PlayZonePosition =
                    FindAnyObjectByType<PlayZoneSpawnPoint>().transform.position;

                levelData.HeroesGridPlacesPosition =
                    FindAnyObjectByType<GridHeroesPlacesSpawnPoint>().transform.position;

                levelData.EnemiesGridPlacesPosition =
                    FindAnyObjectByType<GridEnemiesPlacesSpawnPoint>().transform.position;
                
                levelData.LevelKey = SceneManager.GetActiveScene().name;
            }

            EditorUtility.SetDirty(target);
        }
    }
}