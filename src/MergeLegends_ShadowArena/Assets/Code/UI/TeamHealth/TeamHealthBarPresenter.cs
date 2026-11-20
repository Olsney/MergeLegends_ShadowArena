using System.Collections.Generic;
using Code.Units;

namespace Code.UI.TeamHealth
{
    public class TeamHealthBarPresenter : Presenter<TeamHealthBarView>
    {
        private readonly List<IHealth> _healths;
        private readonly float _maxHealth;

        public TeamHealthBarPresenter(TeamHealthBarView view, List<IHealth> healths) : base(view)
        {
            _healths = healths;
            _maxHealth = GetTotalMaxHealth();
        }

        public override void Initialize()
        {
            foreach (IHealth health in _healths)
                health.HealthChanged += OnHealthChanged;

            View.SetHealth(GetNormalizedHealth());
        }

        public override void Dispose()
        {
            foreach (IHealth health in _healths)
                health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
            => View.UpdateHealth(GetNormalizedHealth());

        private float GetNormalizedHealth()
            => _maxHealth > 0f ? GetTotalCurrentHealth() / _maxHealth : 0f;

        private float GetTotalCurrentHealth()
        {
            float total = 0f;

            foreach (IHealth health in _healths)
                total += health.CurrentValue;

            return total;
        }

        private float GetTotalMaxHealth()
        {
            float total = 0f;

            foreach (IHealth health in _healths)
                total += health.MaxValue;

            return total;
        }
    }
}