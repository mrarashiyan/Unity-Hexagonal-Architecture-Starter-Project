using Project.Application.Ports.Persistence.Variables;
using Project.Core.Data.StorageData;

namespace Project.Application.Ports.Persistence.Variables
{
    public interface IUserStorage : IStorageVariable
    {
        UserData Data { get; }
    }
}