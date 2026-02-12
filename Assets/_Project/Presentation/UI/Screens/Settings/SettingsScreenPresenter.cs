using Project.Application.Ports.Services;
using Project.Presentation.UI.Screens.Home;

namespace Project.Presentation.UI.Screens.Settings
{
    public class SettingsScreenPresenter
    {
        private IUserInterfaceService _userInterfaceService;
        private ISettingsScreenView _view;

        public SettingsScreenPresenter(ISettingsScreenView view, IUserInterfaceService userInterfaceService)
        {
            _view = view;
            _userInterfaceService = userInterfaceService;
        }

        public void NavigateToBack()
        {
            _userInterfaceService.SwitchScreen(_view.GetType(), typeof(HomeScreenView));
        }
    }
}