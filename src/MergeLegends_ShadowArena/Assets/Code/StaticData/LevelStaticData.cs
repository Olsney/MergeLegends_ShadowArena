using UnityEngine;
using UnityEngine.Serialization;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        [FormerlySerializedAs("HeroesPlacesMarker")] public Vector3 HeroesPlacesMarkerPosition;
    }
}