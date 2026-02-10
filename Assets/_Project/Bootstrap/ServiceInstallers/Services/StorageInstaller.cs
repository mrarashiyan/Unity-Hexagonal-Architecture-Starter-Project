using Cysharp.Threading.Tasks;
using Project.Application.Ports.Persistence.Variables;
using Project.Bootstrap.Base;
using Project.Infrastructure.Persistence.Storage;
using Project.Infrastructure.Persistence.Variables;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class StorageInstaller : BaseServiceInstaller
    {
        public IUserStorage UserStorage { get; private set; }
        
        
        //[SerializeField] private OnlineStorage m_OnlineStorage;
        private LocalStorage _localStorage;
        
        protected override async UniTask InitializeModule()
        {
            // here we can initialize Online Storage, DBs or etc.
            //await m_OnlineStorage.Initialize();
            _localStorage = new LocalStorage();
            await UniTask.Yield();
            
            UserStorage = new UserStorage(_localStorage);
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