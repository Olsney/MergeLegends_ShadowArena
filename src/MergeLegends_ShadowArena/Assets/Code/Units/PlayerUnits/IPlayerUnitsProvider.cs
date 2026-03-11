namespace Code.Units.PlayerUnits
{
    public interface IPlayerUnitsProvider : IUnitsProvider
    {
        void AddUnit(PlayerUnit playerUnit);
        void RemoveUnit(PlayerUnit playerUnit);
    }
}