using System.Collections.Generic;

namespace Code.Units.PlayerUnit
{
    public class PlayerUnitProvider : IPlayerUnitProvider
    {
        private readonly List<PlayerUnit> _units = new List<PlayerUnit>();

        public void AddUnit(PlayerUnit unit) 
            => _units.Add(unit);

        public List<PlayerUnit> GetUnits() 
            => _units;
    }
}