using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class PitchRangeData
    {
        [Range(0.1f, 3f)] public float Min;
        [Range(0.1f, 3f)] public float Max;
    }
}