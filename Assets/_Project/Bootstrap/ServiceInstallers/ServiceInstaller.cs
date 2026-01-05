using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Application.Ports;
using Project.Application.Ports.Persistance.LocalStorage.Interface;
using Project.Bootstrap.Base;
using Project.Bootstrap.Enums;
using Project.Config.Installer;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class ServiceInstaller : MonoBehaviour
    {
        public ILocalStorage LocalStorage { get; private set; }
        public IGameTime GameTime { get; private set; }

        public InstallStatus InstallStatus { get; private set; }
        public Action<float> OnProgress;

        private List<BaseModuleInstaller> _bootsProcessing = new();

        [SerializeField] private ServicesInstallLocator m_ServicesInstallLocator;

        public async UniTask<InstallStatus> Install()
        {
            ReportProgress(0);
            InstallStatus = InstallStatus.InProgress;

            // create objects
            var dummyService = await Instantiate<DummyInstaller>(m_ServicesInstallLocator.DummyInstaller);
            var localStorageService = await Instantiate<LocalStorageInstaller>(m_ServicesInstallLocator.LocalStorage);
            var gameTime = await Instantiate<TimeInstaller>(m_ServicesInstallLocator.TimeInstaller);

            //set dependencies


            //initialize all services
            dummyService.Initialize().Forget();
            localStorageService.Initialize().Forget();
            gameTime.Initialize().Forget();

            ReportProgress(100);
            
            var result = await WaitForFinishingBoot();
            return result ? InstallStatus.Succeeded : InstallStatus.Failed;
        }

        private async UniTask<T> Instantiate<T>(GameObject servicePrefab)
        {
            var serviceGo = await InstantiateAsync(servicePrefab, transform);
            var bootProcess = serviceGo.FirstOrDefault();

            _bootsProcessing.Add(bootProcess.GetComponent<BaseModuleInstaller>());

            return bootProcess.GetComponent<T>();
        }

        private async UniTask<bool> WaitForFinishingBoot()
        {
            foreach (var bootProcess in _bootsProcessing)
            {
                await UniTask.WaitUntil(() => bootProcess.InstallStatus != InstallStatus.InProgress);

                return bootProcess.InstallStatus == InstallStatus.Succeeded || 
                       (bootProcess.TimeoutCanBlockBoot == false && bootProcess.InstallStatus == InstallStatus.TimedOut);
            }

            return true;
        }

        private void ReportProgress(float progress)
        {
            OnProgress?.Invoke(progress);
        }
    }
}