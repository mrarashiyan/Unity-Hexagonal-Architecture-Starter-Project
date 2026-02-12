using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Bootstrap.Base;
using Project.Presentation.UI.Screens.Settings;

namespace Project.Bootstrap.ScreenInstallers.Screens
{
    public class SettingsScreenInstaller : BaseScreenInstaller<SettingsScreenView>   {
        protected override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
        {
            var presenter = new SettingsScreenPresenter(Screen, serviceLocator.UserInterface);
            
            Screen.BindDependencies(presenter);
            await Screen.InitializeScreen(eventBus, serviceLocator);
        }
    }
}