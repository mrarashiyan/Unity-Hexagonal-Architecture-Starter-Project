using Project.Application.Ports.Persistence.Variables;

namespace Project.Application.Ports.Services
{
    public interface IStorageService
    {
        IUserStorage UserStorage { get; }
    }
}