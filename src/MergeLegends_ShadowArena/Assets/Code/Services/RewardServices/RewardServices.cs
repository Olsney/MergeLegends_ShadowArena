using System;
using Code.Services.LevelServices;
using Code.Services.PersistentProgress;

namespace Code.Services.RewardServices
{
    public class RewardServices : IRewardServices
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ILevelService _levelService;

        public RewardServices(IPersistentProgressService progressService, ILevelService levelService)
        {
            _progressService = progressService;
            _levelService = levelService;
        }

        public event Action CoinsTransferred;

        public void TransferCoinsVictory()
        {
            AddCoins(true);
        }

        public void TransferCoinsLose()
        {
            AddCoins(false);
        }

        private void AddCoins(bool isVictory)
        {
            int levelIndex = _progressService.Progress.WorldData.LevelData.CurrentLevelIndex;
            var gameLevelData = _levelService.GetLevelData(levelIndex);
            int reward = isVictory ? gameLevelData.VictoryRewardValue : gameLevelData.LoseRewardValue;
            _progressService.Progress.WorldData.CurrencyData.Add(reward);
            CoinsTransferred?.Invoke();
        }
    }
}