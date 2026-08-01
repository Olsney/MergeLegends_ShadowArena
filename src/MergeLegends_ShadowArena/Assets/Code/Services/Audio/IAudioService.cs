using Code.StaticData;

namespace Code.Services.Audio
{
    public interface IAudioService
    {
        void PlaySound(GameSoundTypeId gameSoundTypeId);
        void SetMute(bool muted);
        bool IsMuted { get; }
    }
}