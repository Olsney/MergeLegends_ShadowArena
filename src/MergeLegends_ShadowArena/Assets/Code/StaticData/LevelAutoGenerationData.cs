using System;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class LevelAutoGenerationData
    {
        [Tooltip("Боссовый уровень каждые N уровней")]
        [Min(2)] public int BossEveryNLevels;
    }
}