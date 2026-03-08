using System.Collections.Generic;

namespace Code.Units
{
    public interface IUnitsProvider
    {
        void AddUnit(IUnit unit);
        void RemoveUnit(IUnit unit);
        IReadOnlyList<IUnit> GetUnits();
    }
}