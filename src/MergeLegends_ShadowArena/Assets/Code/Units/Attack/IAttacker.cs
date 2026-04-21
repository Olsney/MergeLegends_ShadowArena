namespace Code.Units.Attack
{
    public interface IAttacker
    {
        bool IsAttacking { get; }
        void StartAttacking();
    }
}