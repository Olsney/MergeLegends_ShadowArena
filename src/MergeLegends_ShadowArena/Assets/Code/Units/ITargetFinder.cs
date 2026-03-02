using Code.Units.Config;

namespace Code.Units
{
    public interface ITargetFinder
    {
        void InitConfig(TargetConfig config);
        Enemy GetTarget();
    }
}