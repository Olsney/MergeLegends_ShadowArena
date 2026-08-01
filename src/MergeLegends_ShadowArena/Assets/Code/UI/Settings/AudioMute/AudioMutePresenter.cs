using Code.Services.Audio;

namespace Code.UI.Settings.AudioMute
{
    public class AudioMutePresenter : Presenter<AudioMuteView>
    {
        private readonly IAudioService _audioService;

        public AudioMutePresenter(AudioMuteView view, IAudioService audioService) : base(view)
        {
            _audioService = audioService;
        }

        public override void Initialize()
        {
            View.Changed += OnChanged;
            View.SetValue(_audioService.IsMuted == false);
        }

        public override void Dispose() 
            => View.Changed -= OnChanged;

        private void OnChanged(bool? value)
        {
            if (value.HasValue == false)
                return;

            _audioService.SetMute(value.Value == false);
        }
    }
}