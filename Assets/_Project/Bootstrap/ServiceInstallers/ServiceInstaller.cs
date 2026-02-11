using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Bootstrap.Enums;
using Project.Bootstrap.Interfaces;
using Project.Config.Installer;
using Project.Infrastructure.Console;
using Project.Infrastructure.GameTime;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class ServiceInstaller : MonoBehaviour
    {
        public InstallStatus InstallStatus { get; private set; }
        public Action<float> OnProgress;

        [SerializeField] private ServicesInstallLocator m_ServicesInstallLocator;
        private List<IServiceInstaller> _bootsProcessing = new();
        private Transform _serviceParent;
        
        public async UniTask<InstallStatus> Install(IEventBus eventBus, IServiceLocator serviceLocator)
        {
            ReportProgress(0);
            InstallStatus = InstallStatus.InProgress;
            
            _serviceParent = new GameObject("Services").transform;
            
            //create default settings
            serviceLocator.Console = new UnityConsole();
            serviceLocator.GameTime = new UnityGameTime();

            // create objects
            var dummyInstaller = await Instantiate<DummyInstaller>(m_ServicesInstallLocator.DummyInstaller);
            var storageInstaller = await Instantiate<StorageInstaller>(m_ServicesInstallLocator.StorageInstaller);
            var gameDesignInstaller = await Instantiate<GameDesignInstaller>(m_ServicesInstallLocator.GameDesignInstaller);

            //set dependencies


            //initialize all services
            dummyInstaller.Initialize(eventBus).Forget();
            storageInstaller.Initialize(eventBus).Forget();
            gameDesignInstaller.Initialize(eventBus).Forget();

            ReportProgress(100);

            var result = await WaitForFinishingBoot();
            if (result)
            {
                // add the services to Locator
                serviceLocator.StorageService = storageInstaller.Service;
                serviceLocator.GameDesignService = gameDesignInstaller.Service;
            }

            return result ? InstallStatus.Succeeded : InstallStatus.Failed;
        }

        private async UniTask<T> Instantiate<T>(GameObject servicePrefab) where T : IServiceInstaller
        {
            var serviceGo = await InstantiateAsync(servicePrefab, _serviceParent);
            var bootProcess = serviceGo.FirstOrDefault();

            var installer = bootProcess.GetComponent<T>();
            _bootsProcessing.Add(installer);

            return installer;
        }

        private async UniTask<bool> WaitForFinishingBoot()
        {
            for (int i = 0; i < _bootsProcessing.Count; i++)
            {
                var process = _bootsProcessing[i];
                await UniTask.WaitUntil(() => process.InstallStatus != InstallStatus.InProgress);

                if (process.InstallStatus == InstallStatus.Failed ||
                    (process.TimeoutCanBlockBoot && process.InstallStatus == InstallStatus.TimedOut))
                    return false;

                ReportProgress(i * 100 / _bootsProcessing.Count);
            }

            return true;
        }

        private void ReportProgress(float progress)
        {
            OnProgress?.Invoke(progress);
        }
    }
}