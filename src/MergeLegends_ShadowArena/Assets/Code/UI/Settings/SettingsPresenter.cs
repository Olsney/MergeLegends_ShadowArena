namespace Code.UI.Settings
{
    public class SettingsPresenter : Presenter<OpenSettingsView>
    {
        private readonly SettingsWindow _settingsWindow;

        public SettingsPresenter(OpenSettingsView view, SettingsWindow settingsSettingsWindow) : base(view)
        {
            _settingsWindow = settingsSettingsWindow;
        }

        public override void Initialize()
            => View.OpenSettingsClicked += OnOpenSettingsClicked;

        public override void Dispose()
            => View.OpenSettingsClicked -= OnOpenSettingsClicked;

        private void OnOpenSettingsClicked()
            => _settingsWindow.Open();
    }
}