using Cysharp.Threading.Tasks;
using Project.Application.Ports.ServiceLocator;

namespace Project.Application.Ports.Screen
{
    public interface IScreenView
    {
        UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator);
        public UniTask ShowScreen();
        public UniTask HideScreen();
    }
}