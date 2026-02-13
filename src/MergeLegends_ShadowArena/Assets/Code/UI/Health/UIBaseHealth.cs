using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.Health
{
    public class UIBaseHealth : MonoBehaviour
    {
        [field: SerializeField] public List<UIHeartMarker> Hearts { get; private set; }
    }
}