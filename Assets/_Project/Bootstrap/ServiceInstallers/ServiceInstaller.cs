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

            // create objects
            var dummyInstaller = await Instantiate<DummyInstaller>(m_ServicesInstallLocator.DummyInstaller);
            var storageInstaller = await Instantiate<StorageInstaller>(m_ServicesInstallLocator.StorageInstaller);
            var userInterfaceInstaller = await Instantiate<UserInterfaceInstaller>(m_ServicesInstallLocator.UserInterfaceInstaller);
            var gameDesignInstaller = await Instantiate<GameDesignInstaller>(m_ServicesInstallLocator.GameDesignInstaller);
            var audioInstaller = await Instantiate<AudioInstaller>(m_ServicesInstallLocator.AudioInstaller);

            //set dependencies


            //initialize all services
            dummyInstaller.Initialize(eventBus, serviceLocator).Forget();
            storageInstaller.Initialize(eventBus, serviceLocator).Forget();
            gameDesignInstaller.Initialize(eventBus, serviceLocator).Forget();
            userInterfaceInstaller.Initialize(eventBus, serviceLocator).Forget();
            audioInstaller.Initialize(eventBus, serviceLocator).Forget();

            ReportProgress(100);

            var result = await WaitForFinishingBoot();
            InstallStatus = result ? InstallStatus.Succeeded : InstallStatus.Failed;
            return InstallStatus;
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