using System;
using System.Collections.Generic;
using Code.Services.LevelAutoGeneratorServices;
using Code.Services.StaticData;
using Code.StaticData;

namespace Code.Services.LevelServices
{
    public class LevelService : ILevelService
    {
        private readonly IStaticDataService _staticData;
        private readonly ILevelAutoGeneratorService _levelAutoGeneratorService;

        public LevelService(IStaticDataService staticData, ILevelAutoGeneratorService levelAutoGeneratorService)
        {
            _staticData = staticData;
            _levelAutoGeneratorService = levelAutoGeneratorService;
        }

        public GameLevelData GetLevelData(int levelIndex)
        {
            if (levelIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(levelIndex));

            List<GameLevelData> handmade = _staticData.GetGameStaticData().GameLevelsData;

            return levelIndex < handmade.Count
                ? handmade[levelIndex]
                : _levelAutoGeneratorService.GetGeneratedLevel(levelIndex);
        }
    }
}