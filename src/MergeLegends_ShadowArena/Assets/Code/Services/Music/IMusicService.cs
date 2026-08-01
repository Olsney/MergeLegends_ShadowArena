namespace Code.Services.Music
{
    public interface IMusicService
    {
        bool IsMuted { get; }
        void SetMute(bool muted);
        void EnableMusic();
    }
}