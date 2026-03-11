namespace Code.Logic.BattleServices
{
    public interface IBattleService
    {
        void StartFight();
        void StopFight();
        bool CanStartFight();
    }
}