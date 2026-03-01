using System.Collections.Generic;

namespace Code.Units.PlayerUnit
{
    public class PlayerUnitProvider : IPlayerUnitProvider
    {
        private readonly List<PlayerUnit> _units = new List<PlayerUnit>();

        public void AddUnit(PlayerUnit unit) 
            => _units.Add(unit);
        
        public void RemoveUnit(PlayerUnit unit) 
            => _units.Remove(unit);

        public IReadOnlyList<PlayerUnit> GetUnits() 
            => _units;
    }
}