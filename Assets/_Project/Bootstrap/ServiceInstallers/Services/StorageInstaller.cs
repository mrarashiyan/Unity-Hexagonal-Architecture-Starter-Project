using Cysharp.Threading.Tasks;
using Project.Application.Ports.Persistence.Variables;
using Project.Bootstrap.Base;
using Project.Infrastructure.Persistence.Storage;
using Project.Infrastructure.Persistence.Variables;
using Project.Presentation.Infrastructures.Persistence;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class StorageInstaller : BaseServiceInstaller
    {
        private StorageService _storageService;
        protected override async UniTask InitializeModule()
        {
            var localStorage = new LocalStorage();
            
            _storageService = gameObject.AddComponent<StorageService>();
            _storageService.BindDependency(localStorage);
            await _storageService.Initialize();
        }

        
    }
}