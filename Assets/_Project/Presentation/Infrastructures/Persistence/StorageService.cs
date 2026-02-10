using Cysharp.Threading.Tasks;
using Project.Application.Ports.Persistence.Storage;
using Project.Application.Ports.Persistence.Variables;
using Project.Infrastructure.Persistence.Storage;
using Project.Infrastructure.Persistence.Variables;
using Project.Presentation.Infrastructures.Base;
using UnityEngine;

namespace Project.Presentation.Infrastructures.Persistence
{
    public class StorageService : BaseService
    {
        public IUserStorage UserStorage { get; private set; }
        
        //private OnlineStorage m_OnlineStorage;
        private ILocalStorage _localStorage;
        
        protected override async UniTask InitializeService()
        {
            // here we can initialize Online Storage, DBs or etc.
            //await m_OnlineStorage.Initialize();

            await UniTask.Yield();
            
            UserStorage = new UserStorage(_localStorage);
        }

        public void BindDependency(ILocalStorage localStorage)
        {
            _localStorage = localStorage;
        }
        
        [ContextMenu("Test")]
        public async void Test()
        {
            UserStorage.Data.Username = "TestUsername";
            UserStorage.Data.NickName = "TestNickName";
            
            await UserStorage.SaveVariable();
            
            Debug.Log(UserStorage.Data.Username);
            Debug.Log(UserStorage.Data.NickName);
        }
    }
}