using System.Collections.Generic;

namespace Code.Units.PlayerUnits
{
    public interface IPlayerUnitProvider
    {
        void AddUnit(PlayerUnit unit);
        void RemoveUnit(PlayerUnit unit);
        IReadOnlyList<PlayerUnit> GetUnits();
    }
}