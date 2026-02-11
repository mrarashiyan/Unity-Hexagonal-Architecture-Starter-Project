using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Bootstrap.Base;
using Project.Bootstrap.Enums;
using Project.Application.Ports.ServiceLocator;
using Project.Bootstrap.Interfaces;
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

        private List<IScreenInstaller> _bootsProcessing = new();
        private Transform _screensParent;

        public async UniTask<InstallStatus> Install(IEventBus eventBus, IServiceLocator serviceLocator)
        {
            ReportProgress(0);
            InstallStatus = InstallStatus.InProgress;

            _screensParent = new GameObject("Screens").transform;
            
            await UniTask.WaitUntil(() => serviceLocator.UserInterface != null);
            
            // create objects
            var loadingOverlay = await Instantiate<LoadingOverlayInstaller>(m_ScreenInstallLocator.LoadingOverlay);
            var dummyScreen = await Instantiate<DummyScreenInstaller>(m_ScreenInstallLocator.DummyScreen);

            //set dependencies


            //initialize all services
            loadingOverlay.Initialize(eventBus, serviceLocator).Forget();
            dummyScreen.Initialize(eventBus, serviceLocator,true).Forget();

            ReportProgress(100);

            var result = await WaitForFinishingBoot();
            InstallStatus = result ? InstallStatus.Succeeded : InstallStatus.Failed;
            return InstallStatus;
        }

        private async UniTask<T> Instantiate<T>(GameObject servicePrefab) where T : IScreenInstaller
        {
            var serviceGo = await InstantiateAsync(servicePrefab, _screensParent);
            var bootProcess = serviceGo.FirstOrDefault();

            var screen = bootProcess.GetComponent<T>();
            _bootsProcessing.Add(screen);
            return screen;
        }

        private async UniTask<bool> WaitForFinishingBoot()
        {
            for (int i = 0; i < _bootsProcessing.Count; i++)
            {
                var process = _bootsProcessing[i];
                await UniTask.WaitUntil(() => process.InstallStatus != InstallStatus.InProgress);

                if (process.InstallStatus == InstallStatus.Failed)
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