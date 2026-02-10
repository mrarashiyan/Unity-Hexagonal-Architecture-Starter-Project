using Project.Application.Ports;
using Project.Application.Ports.Services;

namespace Project.Application.Ports.ServiceLocator
{
    public interface IServiceLocator
    {
        IGameTime GameTime { get; set; }
        IConsole Console { get; set; }
        IStorageService StorageService { get; set; }
    }
}
