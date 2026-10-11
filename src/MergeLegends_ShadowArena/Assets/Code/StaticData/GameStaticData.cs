using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "StaticData/Game")]
    public class GameStaticData : ScriptableObject
    {
        public LocationCatalogData LocationCatalog;
        public FirstGameLaunchData FirstGameLaunchData;
        public List<GameLevelData> GameLevelsData;
        
#if UNITY_EDITOR
        private void OnValidate() =>
            LocationCatalog.UpdateSceneNames();
#endif
    }
}