using Cysharp.Threading.Tasks;
using Project.Application.Ports.ServiceLocator;

namespace Project.Application.Ports.Screen
{
    public interface IScreenView
    {
        UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator);
        public UniTask ShowScreen(bool skipBeforeShow = false, bool skipAfterShow = false);
        public UniTask HideScreen(bool skipBeforeHide = false, bool skipAfterHide = false);
    }
}