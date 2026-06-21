using System.Collections.Generic;

namespace Code.Units.PlayerUnits
{
    public interface IPlayerUnitsProvider
    {
        void AddUnit(PlayerUnit playerUnit);
        void RemoveUnit(PlayerUnit playerUnit);
        IReadOnlyList<PlayerUnit> GetPlayerUnits();
        void Clear();
    }
}