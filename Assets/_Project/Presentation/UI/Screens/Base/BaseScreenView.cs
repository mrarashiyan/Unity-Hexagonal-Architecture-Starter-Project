using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.Screen;
using Project.Application.Ports.ServiceLocator;
using UnityEngine;

namespace Project.Presentation.UI.Screens.Base
{
    public abstract class BaseScreenView : MonoBehaviour, IScreenView
    {
        public abstract UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator);

        protected abstract UniTask BeforeShowScreen();
        protected abstract UniTask AfterShowScreen();
        protected abstract UniTask BeforeHideScreen();
        protected abstract UniTask AfterHideScreen();

        [ContextMenu("Show Screen")]
        public async UniTask ShowScreen(bool skipBeforeShow = false, bool skipAfterShow = false)
        {
            if (!skipBeforeShow)
                await BeforeShowScreen();

            gameObject.SetActive(true);

            if (!skipAfterShow)
                await AfterShowScreen();
        }

        [ContextMenu("Hide Screen")]
        public async UniTask HideScreen(bool skipBeforeHide = false, bool skipAfterHide = false)
        {
            if (!skipBeforeHide)
                await BeforeHideScreen();

            gameObject.SetActive(false);

            if (!skipAfterHide)
                await AfterHideScreen();
        }
    }
}