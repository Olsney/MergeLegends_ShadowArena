using System;

namespace Code.Services.RewardServices
{
    public interface IRewardServices
    {
        event Action CoinsTransferred;
        void TransferCoinsVictory();
        void TransferCoinsLose();
    }
}