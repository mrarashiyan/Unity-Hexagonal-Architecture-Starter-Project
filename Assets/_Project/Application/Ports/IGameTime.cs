using Cysharp.Threading.Tasks;

namespace Project.Application.Ports
{
    public interface IGameTime
    {
        UniTask SetTimeScale(float timeScale);
        UniTask<float> GetTimeScale();
        UniTask<float> GetDeltaTime();
        UniTask<float> GetFixedDeltaTime();
        UniTask<float> GetGameTime();
    }
}