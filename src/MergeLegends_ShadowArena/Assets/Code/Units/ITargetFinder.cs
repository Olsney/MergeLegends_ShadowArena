using Code.StaticData;
using Code.Units.Enemies;

namespace Code.Units
{
    public interface ITargetFinder
    {
        void Init(LayerMaskTypeId layerMaskTypeId);
        Enemy GetTarget();
    }
}