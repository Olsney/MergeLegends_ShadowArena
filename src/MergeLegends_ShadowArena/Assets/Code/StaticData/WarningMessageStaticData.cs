using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Warning message static data", fileName = "WarningMessageStaticData")]
    public class WarningMessageStaticData : ScriptableObject
    {
        public List<WarningMessageData> WarningMessages;
    }
}