using System;
using Code.Services.PersistentProgress;
using Code.Services.StaticData;

namespace Code.Services.RewardServices
{
    public class RewardServices : IRewardServices
    {
        private const string MainSceneName = "Main";

        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _staticData;

        public RewardServices(IPersistentProgressService progressService, IStaticDataService staticData)
        {
            _progressService = progressService;
            _staticData = staticData;
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
            var gameLevelData = _staticData.GetGameStaticData(MainSceneName).GameLevelsData[levelIndex];
            int reward = isVictory ? gameLevelData.VictoryRewardValue : gameLevelData.LoseRewardValue;
            _progressService.Progress.WorldData.CurrencyData.Add(reward);
            CoinsTransferred?.Invoke();
        }
    }
}