using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Bootstrap.Enums;
using UnityEngine;

namespace Project.Bootstrap.Base
{
    public abstract class BaseModuleInstaller : MonoBehaviour
    {
        /// <summary>
        /// Initialize TimeOut in seconds
        /// </summary>
        protected int InitializeTimeout = 5;
        public InstallStatus InstallStatus { get; protected set; }

        public async UniTask<InstallStatus> Initialize()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(InitializeTimeout);

            InstallStatus = InstallStatus.InProgress;
            try
            {
                await InitializeModule();
                InstallStatus = InstallStatus.Succeeded;
            }
            catch (Exception e)
            {
                Debug.LogError($"[ModuleInstallerFailed] Exception={e.Message}");
                
                if(e is TimeoutException)
                    InstallStatus = InstallStatus.TimedOut;
                else
                    InstallStatus = InstallStatus.Failed;
            }
            
            return InstallStatus;
        }
        
        
        protected abstract UniTask InitializeModule();
    }
}