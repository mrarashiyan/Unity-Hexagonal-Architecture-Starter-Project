using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Presentation.Infrastructures.Locator;
using UnityEngine;

namespace Project.Presentation.UI.Screens.Base
{
    public abstract class BaseScreenView : MonoBehaviour
    {
        public abstract UniTask InitializeScreen(IEventBus eventBus, ServiceLocator serviceLocator);
        
        protected abstract UniTask BeforeShowScreen();
        protected abstract UniTask AfterHideScreen();

        public async UniTask ShowScreen()
        {
            await BeforeShowScreen();
            gameObject.SetActive(true);
        }

        public async UniTask HideScreen()
        {
            gameObject.SetActive(false);
            await AfterHideScreen();
        }

    }
}