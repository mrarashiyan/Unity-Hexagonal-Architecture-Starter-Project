namespace Project.Application.Ports.Services
{
    public interface IAudioService:IService
    {
        void MuteAll();
        void UnmuteAll();

        void ToggleMusic(bool isMuted);
        void ToggleSFX(bool isMuted);
    }
}