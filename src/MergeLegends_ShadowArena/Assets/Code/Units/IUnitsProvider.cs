using System.Collections.Generic;

namespace Code.Units
{
    public interface IUnitsProvider
    {
        IReadOnlyList<IUnit> GetUnits();
    }
}