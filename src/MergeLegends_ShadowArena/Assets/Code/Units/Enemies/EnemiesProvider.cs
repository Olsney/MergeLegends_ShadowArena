using System.Collections.Generic;

namespace Code.Units.Enemies
{
    public class EnemiesProvider : IEnemiesProvider
    {
        private readonly List<Enemy> _enemies = new();

        public void AddUnit(Enemy enemy) 
            => _enemies.Add(enemy);
        
        public void RemoveUnit(Enemy enemy) 
            => _enemies.Remove(enemy);

        public IReadOnlyList<IUnit> GetUnits() 
            => _enemies;
    }
}