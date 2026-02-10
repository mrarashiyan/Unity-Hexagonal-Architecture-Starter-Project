using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Bootstrap.Base;
using Project.Bootstrap.Enums;
using Project.Presentation.Infrastructures.Locator;
using Project.Bootstrap.ScreenInstallers.Screens;
using Project.Config.Installer;
using UnityEngine;

namespace Project.Bootstrap.ScreenInstallers
{
    public class ScreenInstaller : MonoBehaviour
    {
        [SerializeField] private ScreenInstallLocator m_ScreenInstallLocator;
        
        public InstallStatus InstallStatus { get; private set; }
        public Action<float> OnProgress;

        private List<BaseScreenInstaller> _bootsProcessing = new();
        
        public async UniTask<InstallStatus> Install(IEventBus eventBus, ServiceLocator serviceLocator)
        {
            ReportProgress(0);
            InstallStatus = InstallStatus.InProgress;

            // create objects
            var dummyScreen = await Instantiate<DummyScreenInstaller>(m_ScreenInstallLocator.DummyScreen);

            //set dependencies


            //initialize all services
            dummyScreen.Initialize(eventBus,serviceLocator).Forget();

            ReportProgress(100);

            var result = await WaitForFinishingBoot();
            return result ? InstallStatus.Succeeded : InstallStatus.Failed;
        }

        private async UniTask<T> Instantiate<T>(GameObject servicePrefab)
        {
            var serviceGo = await InstantiateAsync(servicePrefab, transform);
            var bootProcess = serviceGo.FirstOrDefault();

            _bootsProcessing.Add(bootProcess.GetComponent<BaseScreenInstaller>());

            return bootProcess.GetComponent<T>();
        }

        private async UniTask<bool> WaitForFinishingBoot()
        {

            for (int i = 0; i < _bootsProcessing.Count; i++)
            {
                var process = _bootsProcessing[i];
                await UniTask.WaitUntil(() => process.InstallStatus != InstallStatus.InProgress);
                
                if(process.InstallStatus == InstallStatus.Failed)
                    return false;
                
                ReportProgress(i * 100 /_bootsProcessing.Count);
            }
            
            return true;
        }

        private void ReportProgress(float progress)
        {
            OnProgress?.Invoke(progress);
        }
        
    }
}