using Code.StaticData;

namespace Code.Units
{
    public interface ITargetFinder
    {
        void Init(LayerMaskTypeId layerMaskTypeId);
        IUnit GetTarget();
    }
}