using System.Linq;
using Code.Confetti;
using Code.Logic.PlayArea.Grid;
using Code.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Editor
{
    [CustomEditor(typeof(GameStaticData))]
    public class GameStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Collect from active scene"))
                Collect((GameStaticData)target);
        }

        private static void Collect(GameStaticData gameData)
        {
            string sceneName = SceneManager.GetActiveScene().name;

            LocationSceneEntryData entryData = gameData.LocationCatalog.Entries
                .FirstOrDefault(x => x.Scene != null && x.Scene.name == sceneName);

            if (entryData == null)
            {
                Debug.LogError($"Scene '{sceneName}' is not assigned to any entry in LocationCatalog");
                return;
            }

            GridPlayerUnitPlacesSpawnPoint playerPlaces = FindAnyObjectByType<GridPlayerUnitPlacesSpawnPoint>();
            GridEnemiesPlacesSpawnPoint enemiesPlaces = FindAnyObjectByType<GridEnemiesPlacesSpawnPoint>();

            if (playerPlaces == null || enemiesPlaces == null)
            {
                Debug.LogError("Grid spawn points not found in the active scene");
                return;
            }

            Undo.RecordObject(gameData, "Collect location data");

            entryData.PlayerUnitsGridPlacesPosition = playerPlaces.transform.position;
            entryData.EnemiesGridPlacesPosition = enemiesPlaces.transform.position;

            entryData.ConfettiEffectSpawnPointPosition =
                FindObjectsByType<ConfettiEffectSpawnPoint>(FindObjectsSortMode.None)
                    .Select(x => x.transform.position)
                    .ToList();

            EditorUtility.SetDirty(gameData);
            AssetDatabase.SaveAssets();
        }
    }
}