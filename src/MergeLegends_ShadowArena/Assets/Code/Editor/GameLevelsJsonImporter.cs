using System;
using System.Collections.Generic;
using System.IO;
using Code.StaticData;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    public static class GameLevelsJsonImporter
    {
        [Serializable]
        private class LevelsFile
        {
            public List<LevelJson> levels;
        }

        [Serializable]
        private class LevelJson
        {
            public string location;
            public int victory;
            public int lose;
            public List<EnemyJson> enemies;
        }

        [Serializable]
        private class EnemyJson
        {
            public string unit;
            public int level;
            public int count = 1;
            public int cell = -1;
        }

        [MenuItem("CONTEXT/GameStaticData/Import Levels From JSON")]
        private static void Import(MenuCommand command)
        {
            var data = (GameStaticData)command.context;

            string path = EditorUtility.OpenFilePanel("Game levels JSON", 
                Application.dataPath, "json");
            
            if (string.IsNullOrEmpty(path))
                return;

            LevelsFile file = JsonUtility.FromJson<LevelsFile>(File.ReadAllText(path));
            
            if (file?.levels == null || file.levels.Count == 0)
            {
                Debug.LogError("JSON пустой или не распарсился");
                return;
            }

            var result = new List<GameLevelData>();
            var oldLevels = data.GameLevelsData ?? new List<GameLevelData>();

            for (int i = 0; i < file.levels.Count; i++)
            {
                LevelJson src = file.levels[i];

                var level = new GameLevelData
                {
                    Location = ResolveLocation(src, i, oldLevels, result),
                    VictoryRewardValue = src.victory,
                    LoseRewardValue = src.lose,
                    EnemiesSpawnData = new List<EnemySpawnData>()
                };

                foreach (EnemyJson enemy in src.enemies)
                {
                    if (Enum.TryParse(enemy.unit, out UnitAttackTypeId unitType) == false)
                    {
                        Debug.LogError(
                            $"Уровень {i + 1}: нет значения '{enemy.unit}' в UnitAttackTypeId. Импорт отменён.");
                        return;
                    }

                    if (enemy.cell >= 0 && enemy.count > 1)
                        Debug.LogWarning(
                            $"Уровень {i + 1}: cell={enemy.cell} с count={enemy.count}, юниты окажутся в одной ячейке");

                    for (int c = 0; c < enemy.count; c++)
                        level.EnemiesSpawnData.Add(new EnemySpawnData
                        {
                            LevelIndex = enemy.level,
                            UnitAttackTypeId = unitType,
                            CellIndex = enemy.cell
                        });
                }

                if (level.EnemiesSpawnData.Count > 15)
                    Debug.LogWarning($"Уровень {i + 1}: {level.EnemiesSpawnData.Count} врагов, больше 15");

                result.Add(level);
            }

            Undo.RecordObject(data, "Import game levels");
            data.GameLevelsData = result;
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
            Debug.Log($"Импортировано уровней: {result.Count}");
        }

        private static LocationTypeId ResolveLocation(LevelJson src, int index,
            List<GameLevelData> oldLevels, List<GameLevelData> result)
        {
            if (string.IsNullOrEmpty(src.location) == false)
            {
                if (Enum.TryParse(src.location, out LocationTypeId parsed))
                    return parsed;

                Debug.LogWarning($"Уровень {index + 1}: локации '{src.location}' нет в LocationTypeId");
            }

            if (index < oldLevels.Count)
                return oldLevels[index].Location;

            return result.Count > 0 ? result[result.Count - 1].Location : default;
        }
    }
}