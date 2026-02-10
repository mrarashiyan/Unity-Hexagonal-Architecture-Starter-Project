using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using UnityEngine;
using UnityEngine.LowLevelPhysics2D;

namespace Project.Presentation._Project.Presentation.UI.Screens.Base
{
    public abstract class BaseScreenView : MonoBehaviour
    {
        public abstract UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator);
        
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