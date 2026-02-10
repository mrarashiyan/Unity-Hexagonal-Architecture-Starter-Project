using Cysharp.Threading.Tasks;
using Project.Application.Ports.Persistence.Variables;
using Project.Bootstrap.Base;
using Project.Infrastructure.Persistence.Storage;
using Project.Infrastructure.Persistence.Variables;
using Project.Infrastructure.Persistence;
using Project.Infrastructure.Services;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class StorageInstaller : BaseServiceInstaller<StorageService>
    {
        protected override async UniTask InitializeModule()
        {
            var localStorage = new LocalStorage();
            
            Service = gameObject.AddComponent<StorageService>();
            Service.BindDependency(localStorage);
            await Service.Initialize();
        }

        
    }
}