using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.StaticData
{
    [Serializable]
    public class LocationCatalogData
    {
        public List<LocationSceneEntryData> Entries;

#if UNITY_EDITOR
        public void UpdateSceneNames()
        {
            foreach (LocationSceneEntryData entry in Entries)
                entry.SceneName = entry.Scene != null ? entry.Scene.name : string.Empty;

            foreach (var group in Entries
                         .Where(x => x != null)
                         .GroupBy(x => x.Type)
                         .Where(g => g.Count() > 1))
            {
                UnityEngine.Debug.LogError($"LocationCatalog: duplicate Type '{group.Key}' ({group.Count()} entries)");
            }
        }
#endif
    }
}