using Project.Application.Ports;
using Project.Application.Ports.ServiceLocator;
using Project.Application.Ports.Services;
using Project.Infrastructure.Console;
using Project.Infrastructure.GameTime;
using Project.Infrastructure.Services;

namespace Project.Infrastructure.ServiceLocator
{
    public class ServiceLocator : IServiceLocator
    {
        public IGameTime GameTime { get; private set; } = new UnityGameTime();
        public IConsole Console { get; private set; } = new UnityConsole();

        public IUserInterfaceService UserInterface { get; private set; }
        public IStorageService StorageService { get; private set; }
        public IGameDesignService GameDesignService { get; private set; }
        public ISceneManagement SceneManagement { get; private set; } = new UnitySceneManagement();

        
        public void AddService<TService>(TService service)
        {
            switch (service)
            {
                case IUserInterfaceService userInterfaceService:
                    UserInterface = userInterfaceService;
                    break;

                case IStorageService storage:
                    StorageService = storage;
                    break;
                case IGameDesignService gameDesign:
                    GameDesignService = gameDesign;
                    break;
                case ISceneManagement sceneManagement:
                    SceneManagement = sceneManagement;
                    break;
            }
        }
    }
}