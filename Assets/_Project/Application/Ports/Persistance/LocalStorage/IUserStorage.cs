using Cysharp.Threading.Tasks;
using Project.Application.Ports.Persistance.LocalStorage.Interface;
using Project.Core.Enums;

namespace Project.Application.Ports.Persistance.LocalStorage
{
    public interface IUserStorage : ILocalStorageVariable
    {
        string Username { get; set; }
        string NickName { get; set; }
        
        string GetUsername();
        UniTask SetUsername(string username);
    }
}