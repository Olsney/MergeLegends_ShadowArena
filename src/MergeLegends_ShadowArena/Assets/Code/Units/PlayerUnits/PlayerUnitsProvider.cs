using System.Collections.Generic;

namespace Code.Units.PlayerUnits
{
    public class PlayerUnitsProvider : IPlayerUnitsProvider
    {
        private readonly List<PlayerUnit> _playerUnits = new();

        public void AddUnit(PlayerUnit playerUnit) 
            => _playerUnits.Add(playerUnit);
        
        public void RemoveUnit(PlayerUnit playerUnit)
        {
            if (playerUnit == null)
                return;

            _playerUnits.Remove(playerUnit);
        }

        public IReadOnlyList<PlayerUnit> GetPlayerUnits() 
            => _playerUnits;
        
        public void Clear() 
            => _playerUnits.Clear();
    }
}