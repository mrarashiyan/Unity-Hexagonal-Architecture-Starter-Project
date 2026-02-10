using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Bootstrap.Base;
using Project.Bootstrap.Enums;
using Project.Config.Installer;
using Project.Presentation.Infrastructures.Persistence;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class ServiceInstaller : MonoBehaviour
    {
        public InstallStatus InstallStatus { get; private set; }
        public Action<float> OnProgress;

        private List<BaseServiceInstaller> _bootsProcessing = new();

        [SerializeField] private ServicesInstallLocator m_ServicesInstallLocator;

        public async UniTask<InstallStatus> Install(IEventBus eventBus)
        {
            ReportProgress(0);
            InstallStatus = InstallStatus.InProgress;

            // create objects
            var dummyInstaller = await Instantiate<DummyInstaller>(m_ServicesInstallLocator.DummyInstaller);
            var storageInstaller = await Instantiate<StorageInstaller>(m_ServicesInstallLocator.StorageInstaller);

            //set dependencies


            //initialize all services
            dummyInstaller.Initialize(eventBus).Forget();
            storageInstaller.Initialize(eventBus).Forget();
            
            ReportProgress(100);

            var result = await WaitForFinishingBoot();
            return result ? InstallStatus.Succeeded : InstallStatus.Failed;
        }

        private async UniTask<T> Instantiate<T>(GameObject servicePrefab)
        {
            var serviceGo = await InstantiateAsync(servicePrefab, transform);
            var bootProcess = serviceGo.FirstOrDefault();

            _bootsProcessing.Add(bootProcess.GetComponent<BaseServiceInstaller>());

            return bootProcess.GetComponent<T>();
        }

        private async UniTask<bool> WaitForFinishingBoot()
        {
            for (int i = 0; i < _bootsProcessing.Count; i++)
            {
                var process = _bootsProcessing[i];
                await UniTask.WaitUntil(() => process.InstallStatus != InstallStatus.InProgress);

                if (process.InstallStatus == InstallStatus.Failed ||
                    (process.TimeoutCanBlockBoot && process.InstallStatus == InstallStatus.TimedOut) )
                    return false;
                
                ReportProgress(i * 100/_bootsProcessing.Count);
            }

            return true;
        }

        private void ReportProgress(float progress)
        {
            OnProgress?.Invoke(progress);
        }
    }
}