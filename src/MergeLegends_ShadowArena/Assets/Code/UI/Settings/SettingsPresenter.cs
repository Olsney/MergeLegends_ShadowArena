using Code.Services.Windows;
using Code.UI.Factory;

namespace Code.UI.Settings
{
    public class SettingsPresenter : Presenter<OpenSettingsView>
    {
        private readonly IUIFactory _uiFactory;

        public SettingsPresenter(OpenSettingsView view, IUIFactory uiFactory) : base(view)
        {
            _uiFactory = uiFactory;
        }

        public override void Initialize()
            => View.OpenSettingsClicked += OnOpenSettingsClicked;

        public override void Dispose()
            => View.OpenSettingsClicked -= OnOpenSettingsClicked;

        private void OnOpenSettingsClicked()
            => _uiFactory.CreateWindow(WindowType.Settings);
    }
}