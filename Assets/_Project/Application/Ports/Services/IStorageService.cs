using Project.Application.Ports.Persistence.Variables;

namespace Project.Application.Ports.Services
{
    public interface IStorageService:IService
    {
        IUserStorage UserStorage { get; }
    }
}