using Project.Application.Ports.Persistance.LocalStorage.Interface;

namespace Project.Application.Ports.ServiceLocator
{
    public interface IServiceLocator
    {
        public ILocalStorage LocalStorage { get; }
        public IGameTime GameTime { get; }
    }
}