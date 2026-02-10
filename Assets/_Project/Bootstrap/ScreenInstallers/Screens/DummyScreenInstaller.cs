using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Bootstrap.Base;
using Project.Presentation.Infrastructures.Locator;
using UnityEngine;

namespace Project.Bootstrap.ScreenInstallers.Screens
{
    public class DummyScreenInstaller : BaseScreenInstaller
    {
        protected override async UniTask InitializeScreen(IEventBus eventBus, ServiceLocator serviceLocator)
        {
            
        }
    }
}