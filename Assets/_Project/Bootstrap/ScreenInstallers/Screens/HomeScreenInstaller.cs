using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Bootstrap.Base;
using Project.Presentation.UI.Screens;
using UnityEngine;

namespace Project.Bootstrap.ScreenInstallers.Screens
{
    public class HomeScreenInstaller : BaseScreenInstaller<HomeScreenView>
    {
        protected override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
        {
            await Screen.InitializeScreen(eventBus, serviceLocator);
            await Screen.HideScreen();
        }
    }
}