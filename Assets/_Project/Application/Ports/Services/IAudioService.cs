namespace Project.Application.Ports.Services
{
    public interface IAudioService
    {
        void MuteAll();
        void UnmuteAll();

        void ToggleMusic(bool isMuted);
        void ToggleSFX(bool isMuted);
    }
}