using Code.Logic.PlayArea.GridConfig;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "GridData", menuName = "StaticData/Grid")]
    public class GridPlacesStaticData : ScriptableObject
    {
        public GridPlacesTypeId gridPlacesTypeId;
        public GridConfig GridConfig;
    }
}