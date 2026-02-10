using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Bootstrap.Base;
using UnityEngine;

namespace Project.Bootstrap.ScreenInstallers.Screens
{
    public class DummyScreenInstaller : BaseScreenInstaller
    {
        protected override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
        {
            
        }
    }
}