using System;

namespace Code.Logic.BattleServices
{
    public interface IBattleService
    {
        void StartFight();
        bool CanStartFight();
        event Action FightStarting;
    }
}