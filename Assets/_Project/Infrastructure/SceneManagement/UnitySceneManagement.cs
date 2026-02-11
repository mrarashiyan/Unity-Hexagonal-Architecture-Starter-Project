using Cysharp.Threading.Tasks;
using Project.Application.Ports;
using Project.Infrastructure.Base;
using UnityEngine.SceneManagement;

namespace Project.Infrastructure.Services
{
    public class UnitySceneManagement : ISceneManagement
    {
        public async UniTask LoadSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToUniTask();
            await UniTask.WaitForSeconds(1);
        }

        public async UniTask LoadSceneAdditiveAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).ToUniTask();
            await UniTask.WaitForSeconds(1);
        }

        public async UniTask UnloadSceneAsync(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName).ToUniTask();
            await UniTask.WaitForSeconds(1);
        }
        
    }
}
