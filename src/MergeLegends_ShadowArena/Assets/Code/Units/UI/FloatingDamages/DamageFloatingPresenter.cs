using Code.UI;
using UnityEngine;

namespace Code.Units.UI.FloatingDamages
{
    public class DamageFloatingPresenter : Presenter<DamageFloatingView>
    {
        private readonly IHealth _health;
        private float _lastHealthValue;

        public DamageFloatingPresenter(DamageFloatingView view, IHealth health) : base(view)
        {
            _health = health;
        }

        public override void Initialize()
        {
            _health.HealthChanged += OnHealthChanged;
            _lastHealthValue = _health.CurrentValue;
            View.Hide();
        }

        public override void Dispose()
        {
            _health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            float damage = _lastHealthValue - _health.CurrentValue;
            _lastHealthValue = _health.CurrentValue;

            if (damage > 0)
            {
                View.Show(damage);
            }
        }
    }
}