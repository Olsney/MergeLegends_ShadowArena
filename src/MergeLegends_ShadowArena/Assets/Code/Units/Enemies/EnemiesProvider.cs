using System.Collections.Generic;

namespace Code.Units.Enemies
{
    public class EnemiesProvider : IEnemiesProvider
    {
        private readonly List<Enemy> _enemies = new();

        public void AddUnit(Enemy enemy) 
            => _enemies.Add(enemy);
        
        public void RemoveUnit(Enemy enemy)
        {
            if (enemy == null)
                return;
            
            _enemies.Remove(enemy);
        }

        public IReadOnlyList<Enemy> GetEnemies() 
            => _enemies;
        
        public void Clear() 
            => _enemies.Clear();
    }
}