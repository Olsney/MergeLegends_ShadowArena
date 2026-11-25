using System.Collections.Generic;

namespace Code.Units.Enemies
{
    public interface IEnemiesProvider
    {
        void AddUnit(Enemy enemy);
        void RemoveUnit(Enemy enemy);
        IReadOnlyList<Enemy> GetEnemies();
        void Clear();
    }
}