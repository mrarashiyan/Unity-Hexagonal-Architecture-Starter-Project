using System;
using Cysharp.Threading.Tasks;
using Project.Application.Ports.Persistence.Storage;
using Project.Application.Ports.Persistence.Variables;
using Project.Core.Data.StorageData;
using Project.Core.Enums;

namespace Project.Infrastructure.Persistence.Variables
{
    public class UserStorage : IUserStorage
    {
        public UserData Data { get;}
        private readonly ILocalStorage _localStorage;

        public UserStorage(ILocalStorage localStorage)
        {
            _localStorage = localStorage;
            Data = new UserData();
        }

        public async UniTask<bool> SaveVariable()
        {
            _localStorage.SavePlayerPrefs(LocalStorageKey.NickName, Data.NickName);
            _localStorage.SavePlayerPrefs(LocalStorageKey.UserName, Data.Username);
            return true;
        }

        public async UniTask<bool> LoadVariable()
        {
            Data.Username = (string)_localStorage.LoadPlayerPrefs(LocalStorageKey.UserName, typeof(string), String.Empty);
            Data.NickName = (string)_localStorage.LoadPlayerPrefs(LocalStorageKey.NickName, typeof(string), String.Empty);
            return true;
        }

        public async UniTask<bool> DeleteVariable()
        {
            _localStorage.DeletePlayerPrefs(LocalStorageKey.UserName);
            _localStorage.DeletePlayerPrefs(LocalStorageKey.NickName);
            return true;
        }
    }
}