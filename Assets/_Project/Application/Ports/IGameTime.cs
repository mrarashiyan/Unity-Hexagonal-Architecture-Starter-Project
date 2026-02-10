using Cysharp.Threading.Tasks;

namespace Project.Application.Ports
{
    public interface IGameTime
    {
        void SetTimeScale(float timeScale);
        float GetTimeScale();
        float GetDeltaTime();
        float GetFixedDeltaTime();
        float GetGameTime();
    }
}