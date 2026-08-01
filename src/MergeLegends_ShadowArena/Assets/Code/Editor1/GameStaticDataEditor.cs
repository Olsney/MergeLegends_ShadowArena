using System.Linq;
using Code.Confetti;
using Code.Logic.PlayArea.Grid;
using Code.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor1
{
    [CustomEditor(typeof(GameStaticData))]
    public class GameStaticDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameStaticData gameData = (GameStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                gameData.PlayerUnitsGridPlacesPosition =
                    FindAnyObjectByType<GridPlayerUnitPlacesSpawnPoint>().transform.position;

                gameData.EnemiesGridPlacesPosition =
                    FindAnyObjectByType<GridEnemiesPlacesSpawnPoint>().transform.position;

                gameData.SceneKey = SceneManager.GetActiveScene().name;

                gameData.ConfettiEffectSpawnPointPosition =
                    FindObjectsByType<ConfettiEffectSpawnPoint>(FindObjectsSortMode.None)
                        .Select(x => x.transform.position)
                        .ToList();
            }

            EditorUtility.SetDirty(target);
        }
    }
}