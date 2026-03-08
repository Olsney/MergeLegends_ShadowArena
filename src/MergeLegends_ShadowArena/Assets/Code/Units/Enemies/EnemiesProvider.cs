using System.Collections.Generic;

namespace Code.Units.Enemies
{
    public class EnemiesProvider : IEnemiesProvider
    {
        private readonly List<IUnit> _units = new();

        public void AddUnit(IUnit unit) 
            => _units.Add(unit);
        
        public void RemoveUnit(IUnit unit) 
            => _units.Remove(unit);

        public IReadOnlyList<IUnit> GetUnits() 
            => _units;
    }
}