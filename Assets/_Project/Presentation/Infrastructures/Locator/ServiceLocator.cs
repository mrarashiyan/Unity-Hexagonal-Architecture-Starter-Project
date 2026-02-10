using Project.Application.Ports;
using Project.Presentation.Infrastructures.Persistence;

namespace Project.Presentation.Infrastructures.Locator
{
    public class ServiceLocator
    {
        public IGameTime GameTime;
        public IConsole Console;
        
        public StorageService StorageService;
    }
}