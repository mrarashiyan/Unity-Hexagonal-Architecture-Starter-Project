using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Project.Application.Ports.ServiceLocator;
using Project.Application.Ports.Services;
using Project.Application.UseCases.Level;
using Project.Presentation.UI.Screens.Settings;

namespace Project.Presentation.UI.Screens.Home
{
    /// <summary>
    /// Presenter for HomeScreen. Coordinates between View and UseCases.
    /// </summary>
    public class HomeScreenPresenter
    {
        private readonly IHomeScreenView _view;
        private readonly IGameDesignService _gameDesignService;
        private readonly ILoadLevelUseCase _loadLevelUseCase;
        private readonly IAudioService _audioService;
        private readonly IUserInterfaceService _userInterfaceService;

        private HomeScreenViewModel _viewModel;

        public HomeScreenPresenter(
            IHomeScreenView view,
            IGameDesignService gameDesignService,
            ILoadLevelUseCase loadLevelUseCase,
            IAudioService audioService,
            IUserInterfaceService userInterfaceService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _gameDesignService = gameDesignService ?? throw new ArgumentNullException(nameof(gameDesignService));
            _loadLevelUseCase = loadLevelUseCase ?? throw new ArgumentNullException(nameof(loadLevelUseCase));
            _audioService = audioService ?? throw new ArgumentNullException(nameof(audioService));
            _userInterfaceService =
                userInterfaceService ?? throw new ArgumentNullException(nameof(userInterfaceService));
        }

        /// <summary>
        /// Initialize the presenter and prepare ViewModel.
        /// </summary>
        public async UniTask InitializeAsync()
        {
            await CreateViewModelAsync();
            _view.UpdateUI(_viewModel);
        }

        /// <summary>
        /// Handle level selection button click.
        /// </summary>
        public async UniTask OnLevelSelectedAsync(int levelIndex)
        {
            await _loadLevelUseCase.Execute(levelIndex);
        }

        /// <summary>
        /// Cleanup when screen is hidden.
        /// </summary>
        public void OnCleanup()
        {
            _view.ClearLevels();
        }

        private async UniTask CreateViewModelAsync()
        {
            var levelData = _gameDesignService.LevelData;
            var levels = new List<LevelItemModel>();

            for (int i = 0; i < levelData.LevelCount; i++)
            {
                levels.Add(new LevelItemModel(
                    levelIndex: i,
                    spriteIndex: i,
                    isLocked: false, // TODO: Implement unlock logic based on save data
                    highScore: 0 // TODO: Load from save data
                ));
            }

            _viewModel = new HomeScreenViewModel(levelData.LevelCount, levels, _audioService.IsMasterMute());
            await UniTask.CompletedTask;
        }

        public void ToggleAudioMute(bool value)
        {
            if (value)
            {
                _audioService.MuteAll();
            }
            else
            {
                _audioService.UnmuteAll();
            }
        }

        public void SwitchToSettingsScreen()
        {
            _userInterfaceService.SwitchScreen(_view.GetType(), typeof(SettingsScreenView));
        }
    }
}