using Project.Presentation.UI.Screens.Home;

namespace Project.Presentation.UI.Screens.Home
{
    /// <summary>
    /// Interface that HomeScreenView implements for Presenter communication.
    /// </summary>
    public interface IHomeScreenView
    {
        /// <summary>
        /// Display the levels from ViewModel.
        /// </summary>
        void UpdateUI(HomeScreenViewModel viewModel);
        
        /// <summary>
        /// Clear all level buttons.
        /// </summary>
        void ClearLevels();

        void ToggleAudioMuteIcon(bool isMuted);
        void NavigateToSettingsScreen();
    }
}
