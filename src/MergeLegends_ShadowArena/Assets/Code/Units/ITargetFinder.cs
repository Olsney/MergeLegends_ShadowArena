using Code.StaticData;
using Code.Units.Enemies;

namespace Code.Units
{
    public interface ITargetFinder
    {
        void InitConfig(LayerMaskTypeId layerMaskTypeId);
        Enemy GetTarget();
    }
}