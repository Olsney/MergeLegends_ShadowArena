using Code.Services.Music;

namespace Code.UI.Settings.MusicMute
{
    public class MusicMutePresenter : Presenter<MusicMuteView>
    {
        private readonly IMusicService _musicService;

        public MusicMutePresenter(MusicMuteView view, IMusicService musicService) : base(view)
        {
            _musicService = musicService;
        }

        public override void Initialize()
        {
            View.Changed += OnChanged;
            View.SetValue(_musicService.IsMuted == false);
        }

        public override void Dispose()
            => View.Changed -= OnChanged;

        private void OnChanged(bool? value)
        {
            if (value.HasValue == false)
                return;

            _musicService.SetMute(value.Value == false);
        }
    }
}