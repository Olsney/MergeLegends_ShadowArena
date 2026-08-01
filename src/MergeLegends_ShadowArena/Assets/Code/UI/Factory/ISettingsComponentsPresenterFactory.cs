using Code.UI.Settings.AudioMute;
using Code.UI.Settings.MusicMute;

namespace Code.UI.Factory
{
    public interface ISettingsComponentsPresenterFactory
    {
        AudioMutePresenter CreateAudioMutePresenter(AudioMuteView view);
        MusicMutePresenter CreateMusicMutePresenter(MusicMuteView view);
    }
}