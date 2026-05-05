using Code.StaticData;

namespace Code.UI.SpawnUnits
{
    public interface IPlayerUnitSpawner
    {
        void Spawn(UnitAttackTypeId attackTypeId);
    }
}