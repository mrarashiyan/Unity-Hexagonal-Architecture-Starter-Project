using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Presentation.UI.Screens.Base;
using Project.Presentation.Infrastructures.Locator;
using UnityEngine;

namespace Project.Presentation.UI.Screens
{
    public class DummyScreenView : BaseScreenView
    {
        public override async UniTask InitializeScreen(IEventBus eventBus, ServiceLocator serviceLocator)
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