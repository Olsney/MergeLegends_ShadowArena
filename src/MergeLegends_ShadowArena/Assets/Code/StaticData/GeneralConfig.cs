using System;
using Code.StaticData;
using Code.Units.Config;
using Code.Units.PlayerUnit.Config;

namespace Code.StaticData
{
    [Serializable]
    public class GeneralConfig
    {
        public UnitConfig UnitConfig;
        public TargetConfig TargetConfig;
        public MagicSphereConfig MagicSphereConfig;
    }
}