using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Presentation.UI.Screens.Base;
using UnityEngine;

namespace Project.Presentation.UI.Screens
{
    public class DummyScreenView : BaseScreenView
    {
        public override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
        {
            
        }

        protected override async UniTask BeforeShowScreen()
        {
            
        }

        protected override async UniTask AfterHideScreen()
        {
            
        }
    }
}