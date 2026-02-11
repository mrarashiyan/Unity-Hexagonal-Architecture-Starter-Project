using Project.Application.Ports;
using Project.Application.Ports.Services;

namespace Project.Application.Ports.ServiceLocator
{
    public interface IServiceLocator
    {
        IGameTime GameTime { get;  }
        IConsole Console { get;}
        IUserInterfaceService UserInterface { get;}
        IStorageService StorageService { get;}
        IGameDesignService GameDesignService { get;}
        ISceneManagement SceneManagement { get; }
        void AddService<TService>(TService service);
    }
}
