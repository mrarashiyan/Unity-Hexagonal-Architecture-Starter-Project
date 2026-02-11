using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.Screen;
using Project.Application.Ports.ServiceLocator;
using UnityEngine;

namespace Project.Presentation.UI.Screens.Base
{
    public abstract class BaseScreenView : MonoBehaviour,IScreenView
    {
        public abstract UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator);
        
        protected abstract UniTask BeforeShowScreen();
        protected abstract UniTask AfterHideScreen();

        [ContextMenu("Show Screen")]
        public async UniTask ShowScreen()
        {
            await BeforeShowScreen();
            gameObject.SetActive(true);
        }

        [ContextMenu("Hide Screen")]
        public async UniTask HideScreen()
        {
            gameObject.SetActive(false);
            await AfterHideScreen();
        }

    }
}