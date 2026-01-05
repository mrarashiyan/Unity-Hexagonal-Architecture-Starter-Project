using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Project.Bootstrap.Base;
using Project.Presentation.Infrastructures.Persistance;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class LocalStorageInstaller : BaseModuleInstaller
    {
        [SerializeField] private LocalStorage m_LocalStorage;
        
        protected override async UniTask InitializeModule()
        {
            await m_LocalStorage.Initialize();
        }

        [ContextMenu("Test")]
        public void Test()
        {
            m_LocalStorage.User.Username = "TestUsername";
            m_LocalStorage.User.NickName = "TestNickName";
            
            Debug.Log(m_LocalStorage.User.Username);
            Debug.Log(m_LocalStorage.User.NickName);
        }
    }
}