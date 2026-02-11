using Cysharp.Threading.Tasks;

namespace Project.Application.Ports
{
    /// <summary>
    /// Service for managing Unity scene operations.
    /// </summary>
    public interface ISceneManagement
    {
        /// <summary>
        /// Loads a scene asynchronously in single mode (unloads current scene).
        /// </summary>
        /// <param name="sceneName">Name of the scene to load</param>
        UniTask LoadSceneAsync(string sceneName);

        /// <summary>
        /// Loads a scene asynchronously in additive mode (keeps current scene loaded).
        /// </summary>
        /// <param name="sceneName">Name of the scene to load</param>
        UniTask LoadSceneAdditiveAsync(string sceneName);

        /// <summary>
        /// Unloads a scene asynchronously.
        /// </summary>
        /// <param name="sceneName">Name of the scene to unload</param>
        UniTask UnloadSceneAsync(string sceneName);
    }
}
