using System;

namespace Code.Logic.BattleServices
{
    public interface IBattleService
    {
        void StartFight();
        event Action FightStarting;
    }
}