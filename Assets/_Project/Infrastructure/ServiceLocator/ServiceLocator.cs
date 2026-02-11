using Project.Application.Ports;
using Project.Application.Ports.ServiceLocator;
using Project.Application.Ports.Services;
using Project.Infrastructure.Console;
using Project.Infrastructure.GameTime;

namespace Project.Infrastructure.ServiceLocator
{
    public class ServiceLocator : IServiceLocator
    {
        public IGameTime GameTime { get; private set; } = new UnityGameTime();
        public IConsole Console { get; private set; } = new UnityConsole();
        public IStorageService StorageService { get; private set; }
        public IGameDesignService GameDesignService { get; private set; }

        public void AddService<TService>(TService service)
        {
            switch (service)
            {
                case IGameTime gameTime:
                    GameTime = gameTime;
                    break;
                case IConsole console:
                    Console = console;
                    break;
                case IStorageService storage:
                    StorageService = storage;
                    break;
                case IGameDesignService gameDesign:
                    GameDesignService = gameDesign;
                    break;
            }
        }
    }
}