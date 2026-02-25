using System.Collections.Generic;

namespace Code.Units.PlayerUnit
{
    public interface IPlayerUnitProvider
    {
        void AddUnit(PlayerUnit unit);
        List<PlayerUnit> GetUnits();
    }
}