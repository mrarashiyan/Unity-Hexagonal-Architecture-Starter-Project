using Project.Application.Ports;
using Project.Application.Ports.ServiceLocator;
using Project.Application.Ports.Services;

namespace Project.Infrastructure.ServiceLocator
{
    public class ServiceLocator : IServiceLocator
    {
        public IGameTime GameTime { get; set; }
        public IConsole Console { get; set; }
        public IStorageService StorageService { get; set; }
    }
}