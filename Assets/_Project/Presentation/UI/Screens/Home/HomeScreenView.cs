using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Presentation._Project.Presentation.UI.Widgets;
using Project.Presentation.UI.Screens.Base;
using Project.Presentation.UI.Screens.Home;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Presentation.UI.Screens.Home
{
    /// <summary>
    /// Passive View for Home Screen. Only handles Unity-specific UI operations.
    /// </summary>
    public class HomeScreenView : BaseScreenView, IHomeScreenView
    {
        [SerializeField] private LevelSelectButtonView _levelSelectButtonPrefab;
        [SerializeField] private Transform _levelContainer;
        [SerializeField] private Sprite[] _levelImages;
        [SerializeField] private Toggle _muteToggle;
        [SerializeField] private Button _settingsButton;
        

        private HomeScreenPresenter _presenter;
        private readonly List<LevelSelectButtonView> _activeButtons = new();

        private void OnEnable()
        {
            _muteToggle.onValueChanged.AddListener(OnMuteTogglePressed);
        }

        private void OnDisable()
        {
            _muteToggle.onValueChanged.RemoveListener(OnMuteTogglePressed);
            
        }

        public void BindPresenter(HomeScreenPresenter presenter)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
        {
            if(eventBus == null)
                throw new NullReferenceException("eventBus is Null");

            if (serviceLocator == null)
                throw new NullReferenceException("serviceLocator is Null");
        }

        protected override async UniTask BeforeShowScreen()
        {
            await _presenter.InitializeAsync();
        }

        protected override async UniTask AfterShowScreen()
        {
            // Placeholder for animations or post-show logic
        }

        protected override async UniTask BeforeHideScreen()
        {
            // Placeholder for pre-hide animations
        }

        protected override async UniTask AfterHideScreen()
        {
            _presenter.OnCleanup();
        }

        // IHomeScreenView Implementation

        public void UpdateUI(HomeScreenViewModel viewModel)
        {
            ClearLevels();
            
            ToggleAudioMuteIcon(viewModel.IsAudioMute);

            foreach (var level in viewModel.Levels)
            {
                var btn = Instantiate(_levelSelectButtonPrefab, _levelContainer);
                var buttonView = btn.GetComponent<LevelSelectButtonView>();

                // Set the sprite (with bounds check)
                if (level.SpriteIndex >= 0 && level.SpriteIndex < _levelImages.Length)
                {
                    buttonView.SetBackground(_levelImages[level.SpriteIndex]);
                }

                buttonView.levelIndex = level.LevelIndex;
                buttonView.OnButtonClick += OnLevelButtonClicked;
                _activeButtons.Add(buttonView);
            }
        }

        public void ClearLevels()
        {
            foreach (var button in _activeButtons)
            {
                if (button != null)
                {
                    button.OnButtonClick -= OnLevelButtonClicked;
                    Destroy(button.gameObject);
                }
            }
            _activeButtons.Clear();
        }

        public void ToggleAudioMuteIcon(bool isMuted)
        {
            _muteToggle.SetIsOnWithoutNotify(isMuted);
        }

        public void NavigateToSettingsScreen()
        {
            _presenter.SwitchToSettingsScreen();
        }

        private async void OnLevelButtonClicked(int levelIndex)
        {
            await _presenter.OnLevelSelectedAsync(levelIndex);
        }
        
        private void OnMuteTogglePressed(bool value)
        {
            _presenter.ToggleAudioMute(value);
        }
    }
}