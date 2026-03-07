using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "GridData", menuName = "StaticData/Grid")]
    public class GridPlacesStaticData : ScriptableObject
    {
        public GridPlacesTypeId GridPlacesTypeId;
        public GridData GridData;
    }
}