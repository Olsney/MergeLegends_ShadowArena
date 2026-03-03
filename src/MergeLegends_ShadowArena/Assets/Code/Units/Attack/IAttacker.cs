using Code.Units.Config;
using Code.Units.PlayerUnit.Config;

namespace Code.Units.Attack
{
    public interface IAttacker
    {
        bool IsAttacking { get; }
        void InitConfig(TargetConfig targetConfig, UnitConfig unitConfig);
        void Tick();
    }
}