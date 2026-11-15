using System;
using System.Collections.Generic;
using Code.Services.StaticData;
using Code.Services.UnitStatsServices;
using Code.StaticData;

namespace Code.Services.LevelAutoGeneratorServices
{
    public class LevelAutoGeneratorService : ILevelAutoGeneratorService
    {
        private const int MaxLevelIndexOffset = 90;

        private readonly IStaticDataService _staticData;
        private readonly IUnitStatsService _unitStats;

        private List<GameLevelData> _handmadeLevels;
        private UnitStaticData _commonEnemyData;
        private UnitStaticData _bossData;
        private int _cycleLength;
        private int _maxEnemies;
        private double _powerGrowthPerLevel;
        private double _rewardGrowthPerCycle;

        public LevelAutoGeneratorService(IStaticDataService staticData, IUnitStatsService unitStats)
        {
            _staticData = staticData;
            _unitStats = unitStats;
        }

        public GameLevelData GetGeneratedLevel(int levelIndex)
        {
            if (levelIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(levelIndex));

            EnsureInitialized();

            int handmadeCount = _handmadeLevels.Count;

            if (levelIndex < handmadeCount)
                return _handmadeLevels[levelIndex];

            int templateIndex = handmadeCount - _cycleLength + (levelIndex - handmadeCount) % _cycleLength;
            GameLevelData template = _handmadeLevels[templateIndex];
            int levelsAhead = levelIndex - templateIndex;

            double powerMultiplier = Math.Pow(_powerGrowthPerLevel, levelsAhead);
            double victoryReward = template.VictoryRewardValue *
                                   Math.Pow(_rewardGrowthPerCycle, levelsAhead / _cycleLength);
            double loseRewardShare = template.VictoryRewardValue > 0
                ? (double)template.LoseRewardValue / template.VictoryRewardValue
                : 0;

            return new GameLevelData
            {
                Location = template.Location,
                EnemiesSpawnData = CreateEnemies(template, powerMultiplier),
                VictoryRewardValue = GetRewardValue(victoryReward),
                LoseRewardValue = GetRewardValue(victoryReward * loseRewardShare)
            };
        }

        private List<EnemySpawnData> CreateEnemies(GameLevelData template, double powerMultiplier)
        {
            double targetPower = GetCalculatedPower(template) * powerMultiplier;
            List<EnemyGroup> groups = GetGroupEnemies(template.EnemiesSpawnData);
            int levelIndexOffset = CalculateLevelIndexOffset(template, powerMultiplier);

            List<EnemySpawnData> enemies = ComposeEnemies(groups, targetPower, levelIndexOffset);

            while (enemies.Count > _maxEnemies && levelIndexOffset < MaxLevelIndexOffset)
                enemies = ComposeEnemies(groups, targetPower, ++levelIndexOffset);

            for (int i = enemies.Count - 1; i >= 0 && enemies.Count > _maxEnemies; i--)
                if (enemies[i].UnitAttackTypeId != UnitAttackTypeId.Boss)
                    enemies.RemoveAt(i);

            return enemies;
        }

        private List<EnemySpawnData> ComposeEnemies(List<EnemyGroup> groups, double targetPower, int levelIndexOffset)
        {
            double bossPower = 0;
            double commonPower = 0;

            foreach (EnemyGroup group in groups)
            {
                double power = group.Count * GetHealth(group.Type, group.LevelIndex + levelIndexOffset);

                if (group.Type == UnitAttackTypeId.Boss)
                    bossPower += power;
                else
                    commonPower += power;
            }

            double commonScale = commonPower > 0 ? Math.Max(0, targetPower - bossPower) / commonPower : 0;
            var enemies = new List<EnemySpawnData>(_maxEnemies);

            foreach (EnemyGroup group in groups)
            {
                int count = group.Type == UnitAttackTypeId.Boss
                    ? group.Count
                    : (int)Math.Max(1, Math.Min(Math.Round(group.Count * commonScale), _maxEnemies + 1));

                for (int i = 0; i < count; i++)
                    enemies.Add(new EnemySpawnData
                    {
                        LevelIndex = group.LevelIndex + levelIndexOffset,
                        UnitAttackTypeId = group.Type,
                        CellIndex = -1
                    });
            }

            return enemies;
        }

        private int CalculateLevelIndexOffset(GameLevelData template, double powerMultiplier)
        {
            int topLevelIndex = GetTopCommonLevelIndex(template);
            double healthStep = GetHealth(UnitAttackTypeId.Melee, topLevelIndex + 1) /
                                GetHealth(UnitAttackTypeId.Melee, topLevelIndex);

            if (healthStep <= 1.0)
                return 0;

            double offset = Math.Round(Math.Log(powerMultiplier) / Math.Log(healthStep));
            return (int)Math.Max(0, Math.Min(offset, MaxLevelIndexOffset));
        }

        private int GetTopCommonLevelIndex(GameLevelData level)
        {
            int topLevelIndex = 0;

            foreach (EnemySpawnData enemy in level.EnemiesSpawnData)
                if (enemy.UnitAttackTypeId != UnitAttackTypeId.Boss && enemy.LevelIndex > topLevelIndex)
                    topLevelIndex = enemy.LevelIndex;

            return topLevelIndex;
        }

        private List<EnemyGroup> GetGroupEnemies(List<EnemySpawnData> enemies)
        {
            var groups = new List<EnemyGroup>();

            foreach (EnemySpawnData enemy in enemies)
            {
                EnemyGroup group = groups.Find(x =>
                    x.Type == enemy.UnitAttackTypeId && x.LevelIndex == enemy.LevelIndex);

                if (group == null)
                {
                    group = new EnemyGroup(enemy.UnitAttackTypeId, enemy.LevelIndex);
                    groups.Add(group);
                }

                group.Count++;
            }

            return groups;
        }

        private void EnsureInitialized()
        {
            if (_handmadeLevels != null)
                return;

            GameStaticData gameData = _staticData.GetGameStaticData();
            int cycleLength = gameData.LevelAutoGeneration.BossEveryNLevels;
            List<GameLevelData> handmadeLevels = gameData.GameLevelsData;

            if (cycleLength < 2)
                throw new InvalidOperationException("LevelAutoGeneration.BossEveryNLevels must be at least 2");

            if (handmadeLevels == null || handmadeLevels.Count < 2 * cycleLength)
                throw new InvalidOperationException(
                    $"Level auto generation requires at least {2 * cycleLength} handmade levels in GameStaticData");

            _commonEnemyData = GetEnemyData(UnitAttackTypeId.Melee);
            _bossData = GetEnemyData(UnitAttackTypeId.Boss);
            ValidateHealthGrowth(_commonEnemyData);
            ValidateHealthGrowth(_bossData);

            GridData grid = _staticData.GetGridPlaces(UnitTypeId.Enemy).GridData;
            _maxEnemies = grid.Width * grid.Height;
            _cycleLength = cycleLength;
            _powerGrowthPerLevel = GetCalculatedPowerGrowthPerLevel(handmadeLevels, cycleLength);
            _rewardGrowthPerCycle = GetCalculatedRewardGrowthPerCycle(handmadeLevels, cycleLength);
            _handmadeLevels = handmadeLevels;
        }

        private UnitStaticData GetEnemyData(UnitAttackTypeId type) =>
            _staticData.GetEnemy(type) ?? throw new InvalidOperationException($"Enemy static data {type} not found");

        private void ValidateHealthGrowth(UnitStaticData enemyData)
        {
            int lastLevelIndex = enemyData.UnitStatsByLevelIndexSetups.Count - 1;
            double lastHealth = _unitStats.GetStats(enemyData, lastLevelIndex).Hp;
            double nextHealth = _unitStats.GetStats(enemyData, lastLevelIndex + 1).Hp;

            if (lastHealth <= 0 || nextHealth <= lastHealth)
                throw new InvalidOperationException(
                    $"{enemyData.name}: health must be positive and grow beyond the last level, check Hp Growth");
        }

        private double GetCalculatedPowerGrowthPerLevel(List<GameLevelData> levels, int cycleLength)
        {
            int count = levels.Count;
            int windowLength = Math.Min(count / 2 / cycleLength * cycleLength, 4 * cycleLength);

            double recentPower = GetSumPower(levels, count - windowLength, count);
            double previousPower = GetSumPower(levels, count - 2 * windowLength, count - windowLength);

            return previousPower > 0
                ? Math.Max(1.0, Math.Pow(recentPower / previousPower, 1.0 / windowLength))
                : 1.0;
        }

        private double GetCalculatedRewardGrowthPerCycle(List<GameLevelData> levels, int cycleLength)
        {
            int count = levels.Count;

            double recentReward = GetSumVictoryRewards(levels, count - cycleLength, count);
            double previousReward = GetSumVictoryRewards(levels, count - 2 * cycleLength, count - cycleLength);

            return previousReward > 0 ? Math.Max(1.0, recentReward / previousReward) : 1.0;
        }

        private double GetSumPower(List<GameLevelData> levels, int fromIndex, int toIndex)
        {
            double sum = 0;

            for (int i = fromIndex; i < toIndex; i++)
                sum += GetCalculatedPower(levels[i]);

            return sum;
        }

        private double GetSumVictoryRewards(List<GameLevelData> levels, int fromIndex, int toIndex)
        {
            double sum = 0;

            for (int i = fromIndex; i < toIndex; i++)
                sum += levels[i].VictoryRewardValue;

            return sum;
        }

        private double GetCalculatedPower(GameLevelData level)
        {
            double power = 0;

            foreach (EnemySpawnData enemy in level.EnemiesSpawnData)
                power += GetHealth(enemy.UnitAttackTypeId, enemy.LevelIndex);

            return power;
        }

        private double GetHealth(UnitAttackTypeId type, int levelIndex) =>
            _unitStats.GetStats(type == UnitAttackTypeId.Boss ? _bossData : _commonEnemyData, levelIndex).Hp;

        private int GetRewardValue(double value) =>
            value >= int.MaxValue ? int.MaxValue : (int)Math.Round(value);
    }
}