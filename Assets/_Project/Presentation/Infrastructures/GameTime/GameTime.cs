using Cysharp.Threading.Tasks;
using Project.Application.Ports;
using Project.Presentation.Infrastructures.Base;
using UnityEngine;

namespace Project.Presentation.Infrastructures.GameTime
{
    public class GameTime : BaseService, IGameTime
    {
        protected override async UniTask InitializeService()
        {
            
        }

        public async UniTask SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }

        public async UniTask<float> GetTimeScale()
        {
            return Time.timeScale;
        }

        public async UniTask<float> GetDeltaTime()
        {
            return Time.deltaTime;
        }

        public async UniTask<float> GetFixedDeltaTime()
        {
            return Time.fixedDeltaTime;
        }

        public async UniTask<float> GetGameTime()
        {
            return Time.time;
        }
        
    }
}