using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Bootstrap.Enums;
using Project.Config.Installer;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class ServiceInstaller : MonoBehaviour
    {
        public InitializeStatus InitializeStatus { get; private set; }
        public Action<float> OnProgress;
        
        [SerializeField] private ServicesInstallLocator m_ServicesInstallLocator;

        public async UniTask Install()
        {
            ReportProgress(0);
            InitializeStatus = InitializeStatus.InProgress;
            
            var dummyService = await Instantiate<DummyInstaller>(m_ServicesInstallLocator.DummyInstaller);
            await dummyService.Initialize();

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