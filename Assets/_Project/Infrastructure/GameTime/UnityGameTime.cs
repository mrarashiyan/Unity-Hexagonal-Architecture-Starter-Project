using Cysharp.Threading.Tasks;
using Project.Application.Ports;
using UnityEngine;

namespace Project.Infrastructure.GameTime
{
    public class UnityGameTime:IGameTime
    {
        public void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }

        public float GetTimeScale()
        {
            return Time.timeScale;
        }

        public float GetDeltaTime()
        {
            return Time.deltaTime;
        }

        public float GetFixedDeltaTime()
        {
            return Time.fixedDeltaTime;
        }

        public float GetGameTime()
        {
            return Time.time;
        }

    }
}