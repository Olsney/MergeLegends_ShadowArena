using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "GridData", menuName = "StaticData/Grid")]
    public class GridPlacesStaticData : ScriptableObject
    {
        public UnitTypeId UnitTypeId;
        public GridData GridData;
    }
}