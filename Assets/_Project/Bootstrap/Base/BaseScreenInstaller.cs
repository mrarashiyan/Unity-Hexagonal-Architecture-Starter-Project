using System;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Bootstrap.Enums;
using Project.Presentation.Infrastructures.Locator;
using UnityEngine;

namespace Project.Bootstrap.Base
{
    public abstract class BaseScreenInstaller : MonoBehaviour
    {
        public InstallStatus InstallStatus { get; protected set; }


        public async UniTask<InstallStatus> Initialize(IEventBus eventBus,ServiceLocator serviceLocator)
        {
            Debug.Log($"[{GetType().Name}] Initialize: Started");

            InstallStatus = InstallStatus.InProgress;
            try
            {
                await InitializeScreen(eventBus,serviceLocator);
                InstallStatus = InstallStatus.Succeeded;
            }
            catch (Exception e)
            {
                Debug.LogError($"[{GetType().Name}] Exception={e.Message}");
                InstallStatus = InstallStatus.Failed;
            }

            Debug.Log($"[{nameof(BaseScreenInstaller)}] Initialize: Finished - Result: {InstallStatus}");
            return InstallStatus;
        }


        protected abstract UniTask InitializeScreen(IEventBus eventBus,ServiceLocator serviceLocator);
    }
}