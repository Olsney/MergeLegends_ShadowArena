using System;
using Code.Units.Config;
using Code.Units.PlayerUnit.Config;
using UnityEngine;

namespace Code.StaticData
{
    [Serializable]
    public class GeneralConfig
    {
        public UnitConfig UnitConfig;
        public TargetConfig TargetConfig;
        public GameObject MagicSpherePrefab;
    }
}