namespace Code.Units
{
    public interface ITargetFinder
    {
        void InitConfig(TargetFinderConfig config);
        Enemy GetTarget();
    }
}