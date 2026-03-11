namespace Code.Units.Enemies
{
    public interface IEnemiesProvider : IUnitsProvider
    {
        void AddUnit(Enemy enemy);
        void RemoveUnit(Enemy enemy);
    }
}