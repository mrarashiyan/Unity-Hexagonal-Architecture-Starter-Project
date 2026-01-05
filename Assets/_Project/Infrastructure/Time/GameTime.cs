using Cysharp.Threading.Tasks;
using Project.Application.Ports;

namespace Project.Infrastructure.Time
{
    public abstract class GameTime:IGameTime
    {
        public abstract UniTask SetTimeScale(float timeScale);

        public abstract UniTask<float> GetTimeScale();

        public abstract UniTask<float> GetDeltaTime();

        public abstract UniTask<float> GetFixedDeltaTime();

        public abstract UniTask<float> GetGameTime();

    }
}