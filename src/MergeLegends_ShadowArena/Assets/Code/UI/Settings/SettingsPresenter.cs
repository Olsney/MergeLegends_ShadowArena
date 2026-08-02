using Code.Services.Windows;
using Code.UI.Factory;
using Code.UI.Settings.AudioMute;
using Code.UI.Settings.MusicMute;

namespace Code.UI.Settings
{
    public class SettingsPresenter : Presenter<OpenSettingsView>
    {
        private readonly IUIFactory _uiFactory;
        private readonly ISettingsComponentsPresenterFactory _settingsComponentsPresenterFactory;

        public SettingsPresenter(OpenSettingsView view, IUIFactory uiFactory,
            ISettingsComponentsPresenterFactory settingsComponentsPresenterFactory) : base(view)
        {
            _uiFactory = uiFactory;
            _settingsComponentsPresenterFactory = settingsComponentsPresenterFactory;
        }

        public override void Initialize()
            => View.OpenSettingsClicked += OnOpenSettingsClicked;

        public override void Dispose()
            => View.OpenSettingsClicked -= OnOpenSettingsClicked;

        private void OnOpenSettingsClicked()
        {
            var settingsWindow = _uiFactory.CreateWindow(WindowType.Settings);

            _settingsComponentsPresenterFactory.CreateAudioMutePresenter(settingsWindow
                .GetComponentInChildren<AudioMuteView>());
            
            _settingsComponentsPresenterFactory.CreateMusicMutePresenter(settingsWindow
                .GetComponentInChildren<MusicMuteView>());
        }
    }
}