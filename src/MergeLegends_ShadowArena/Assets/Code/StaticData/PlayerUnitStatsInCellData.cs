using System;

namespace Code.StaticData
{
    [Serializable]
    public class PlayerUnitStatsInCellData
    {
        public int CellIndex;
        public int PlayerUnitLevelIndex;
        public UnitAttackTypeId UnitAttackTypeId;
    }
}