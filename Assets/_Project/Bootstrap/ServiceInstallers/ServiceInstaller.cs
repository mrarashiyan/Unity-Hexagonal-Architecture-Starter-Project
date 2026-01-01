using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Application.Ports.Persistance.LocalStorage.Interface;
using Project.Bootstrap.Enums;
using Project.Config.Installer;
using Project.Presentation.Infrastructures.Persistance;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class ServiceInstaller : MonoBehaviour
    {
        public ILocalStorage LocalStorage { get; private set; }
        
        public InstallStatus InstallStatus { get; private set; }
        public Action<float> OnProgress;
        
        [SerializeField] private ServicesInstallLocator m_ServicesInstallLocator;

        public async UniTask Install()
        {
            ReportProgress(0);
            InstallStatus = InstallStatus.InProgress;
            
            var dummyService = await Instantiate<DummyInstaller>(m_ServicesInstallLocator.DummyInstaller);
            var localStorageService = await Instantiate<LocalStorageInstaller>(m_ServicesInstallLocator.LocalStorage);
            
            
            dummyService.Initialize().Forget();
            localStorageService.Initialize().Forget();

            ReportProgress(100);
        }

        private async UniTask<T> Instantiate<T>(GameObject servicePrefab)
        {
            var serviceGO = await InstantiateAsync(servicePrefab,transform);
            return serviceGO.FirstOrDefault().GetComponent<T>();
        }
        
        private void ReportProgress(float progress)
        {
            OnProgress?.Invoke(progress);
        }
    }
}