using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Application.UseCases.Level;
using Project.Bootstrap.Base;
using Project.Presentation.UI.Screens;
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

            await Screen.InitializeScreen(eventBus, serviceLocator);

            // Create and inject the use case
            var loadLevelUseCase = new LoadLevelUseCase(
                serviceLocator.GameDesignService,
                serviceLocator.SceneManagement,
                serviceLocator.UserInterface);
            Screen.BindDependencies(loadLevelUseCase);

            await Screen.HideScreen(true, true);
        }
    }
}