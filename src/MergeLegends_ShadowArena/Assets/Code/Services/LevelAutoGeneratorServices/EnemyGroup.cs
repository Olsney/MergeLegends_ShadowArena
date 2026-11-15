using Code.StaticData;

namespace Code.Services.LevelAutoGeneratorServices
{
    public class EnemyGroup
    {
        public readonly UnitAttackTypeId Type;
        public readonly int LevelIndex;
        public int Count;

        public EnemyGroup(UnitAttackTypeId type, int levelIndex)
        {
            Type = type;
            LevelIndex = levelIndex;
        }
    }
}