using Code.StaticData;

namespace Code.Units.Attack
{
    public interface IAttacker
    {
        bool IsAttacking { get; }
        void InitConfig(float damage, float cooldown, LayerMaskTypeId targetLayerMask);
        void Tick();
    }
}