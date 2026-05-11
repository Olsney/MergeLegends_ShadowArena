using Code.Logic.PlayArea;
using Code.Logic.PlayArea.Grid;
using Code.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor1
{
    [CustomEditor(typeof(GameStaticData))]
    public class LevelStaticDataEdtior : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameStaticData gameData = (GameStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                gameData.PlayZonePosition =
                    FindAnyObjectByType<PlayZoneSpawnPoint>().transform.position;

                gameData.PlayerUnitsGridPlacesPosition =
                    FindAnyObjectByType<GridPlayerUnitPlacesSpawnPoint>().transform.position;

                gameData.EnemiesGridPlacesPosition =
                    FindAnyObjectByType<GridEnemiesPlacesSpawnPoint>().transform.position;
                
                gameData.SceneKey = SceneManager.GetActiveScene().name;
            }

            EditorUtility.SetDirty(target);
        }
    }
}