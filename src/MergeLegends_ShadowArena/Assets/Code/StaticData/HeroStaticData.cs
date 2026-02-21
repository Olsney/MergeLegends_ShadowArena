using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero")]
    public class HeroStaticData : ScriptableObject
    {
        public HeroTypeId HeroTypeId;
        public HeroConfig HeroConfig;
    }
}