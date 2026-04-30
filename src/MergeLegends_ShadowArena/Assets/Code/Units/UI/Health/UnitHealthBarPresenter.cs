using Code.UI;

namespace Code.Units.UI.Health
{
    public class UnitHealthBarPresenter : Presenter<UnitHealthBarView>
    {
        private readonly IHealth _health;
        private bool _isActive;

        public UnitHealthBarPresenter(UnitHealthBarView view, IHealth health) : base(view)
        {
            _health = health;
        }

        public override void Initialize()
        {
            View.Hide();
            _health.HealthChanged += OnHealthChanged;
            View.UpdateHealth(_health.CurrentValue / _health.MaxValue);
        }

        public override void Dispose() 
            => _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_isActive == false && _health.CurrentValue < _health.MaxValue)
            {
                View.Show();
                _isActive = true;
            }

            View.UpdateHealth(_health.CurrentValue / _health.MaxValue);
        }
    }
}