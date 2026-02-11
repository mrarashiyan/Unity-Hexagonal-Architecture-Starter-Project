using System;
using Cysharp.Threading.Tasks;
using Project.Application.Ports;
using Project.Application.Ports.Services;

namespace Project.Application.UseCases.Level
{
    public class LoadLevelUseCase : ILoadLevelUseCase
    {
        private readonly IGameDesignService _gameDesignService;
        private readonly ISceneManagement _sceneManagement;
        private readonly IUserInterfaceService _userInterfaceService;

        public LoadLevelUseCase(IGameDesignService gameDesignService, ISceneManagement sceneManagementService, IUserInterfaceService userInterfaceService)
        {
            _gameDesignService = gameDesignService ?? throw new ArgumentNullException(nameof(gameDesignService));
            _sceneManagement = sceneManagementService ?? throw new ArgumentNullException(nameof(sceneManagementService));
            _userInterfaceService = userInterfaceService ?? throw new ArgumentNullException(nameof(userInterfaceService));
        }

        public async UniTask Execute(int levelIndex)
        {
            // Validate level index
            if (levelIndex < 0 || levelIndex >= _gameDesignService.LevelData.LevelCount)
            {
                throw new ArgumentOutOfRangeException(nameof(levelIndex),
                    $"Level index {levelIndex} is out of range. Valid range: 0 to {_gameDesignService.LevelData.LevelCount - 1}");
            }

            // Build the scene name based on your naming convention
            string sceneName = $"Level_{levelIndex.ToString("00")}";

            await _userInterfaceService.TransitionIn();
            // Load the scene asynchronously
            await _sceneManagement.LoadSceneAdditiveAsync(sceneName);
            
            await _userInterfaceService.TransitionOut();
        }
    }
}
