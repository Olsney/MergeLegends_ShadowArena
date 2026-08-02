using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "StaticData/Game")]
    public class GameStaticData : ScriptableObject
    {
        public string SceneKey;
        public Vector3 PlayerUnitsGridPlacesPosition;
        public Vector3 EnemiesGridPlacesPosition;
        public List<Vector3> ConfettiEffectSpawnPointPosition;
        public FirstGameLaunchData FirstGameLaunchData;
        public List<GameLevelData> GameLevelsData;
    }
}