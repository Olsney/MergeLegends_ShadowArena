using System.Collections.Generic;

namespace Code.Units.PlayerUnits
{
    public class PlayerUnitProvider : IPlayerUnitProvider
    {
        private readonly List<PlayerUnit> _units = new();

        public void AddUnit(PlayerUnit unit) 
            => _units.Add(unit);
        
        public void RemoveUnit(PlayerUnit unit) 
            => _units.Remove(unit);

        public IReadOnlyList<PlayerUnit> GetUnits() 
            => _units;
    }
}