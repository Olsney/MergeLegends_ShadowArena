namespace Code.Logic
{
    public interface IBattleService
    {
        void StartFight();
        void StopFight();
        bool CanStartFight();
    }
}