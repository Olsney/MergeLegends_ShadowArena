using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "CameraMoveData", menuName = "StaticData/Camera move")]
    public class CameraMoveStaticData : ScriptableObject
    {
        public List<CameraMoveData> CameraMoveDatas;
    }
}