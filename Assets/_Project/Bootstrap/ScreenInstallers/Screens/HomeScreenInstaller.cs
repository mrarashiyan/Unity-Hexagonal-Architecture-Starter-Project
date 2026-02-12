using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Application.UseCases.Level;
using Project.Bootstrap.Base;
using Project.Presentation.UI.Screens;
using Project.Presentation.UI.Screens.Home;
using UnityEngine;

namespace Project.Bootstrap.ScreenInstallers.Screens
{
    public class HomeScreenInstaller : BaseScreenInstaller<HomeScreenView>
    {
        protected override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
        {
            // Wait for dependencies
            await UniTask.WaitUntil(() => serviceLocator.GameDesignService != null);
            await UniTask.WaitUntil(() => serviceLocator.SceneManagement != null);
            await UniTask.WaitUntil(() => serviceLocator.UserInterface != null);

            await Screen.InitializeScreen(eventBus, serviceLocator);

            // Create dependencies
            var loadLevelUseCase = new LoadLevelUseCase(
                serviceLocator.GameDesignService,
                serviceLocator.SceneManagement,
                serviceLocator.UserInterface);

            var presenter = new HomeScreenPresenter(
                Screen,
                serviceLocator.GameDesignService,
                loadLevelUseCase,
                serviceLocator.AudioService,
                serviceLocator.UserInterface);

            // Inject presenter into view
            Screen.BindPresenter(presenter);

            await Screen.HideScreen(true, true);
        }
    }
}