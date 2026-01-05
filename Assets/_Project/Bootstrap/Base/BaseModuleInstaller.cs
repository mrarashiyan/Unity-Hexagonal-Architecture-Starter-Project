using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Bootstrap.Enums;
using UnityEngine;

namespace Project.Bootstrap.Base
{
    public abstract class BaseModuleInstaller : MonoBehaviour
    {

        public bool TimeoutCanBlockBoot { get; private set; }
        public InstallStatus InstallStatus { get; protected set; }

        public async UniTask<InstallStatus> Initialize(int timeout = 5, bool timeoutCanBlockBoot = false)
        {
            TimeoutCanBlockBoot = timeoutCanBlockBoot;
            
            var cts = new CancellationTokenSource();
            cts.CancelAfter(timeout);

            InstallStatus = InstallStatus.InProgress;
            try
            {
                await InitializeModule();
                InstallStatus = InstallStatus.Succeeded;
            }
            catch (Exception e)
            {
                Debug.LogError($"[{GetType().Name}] Exception={e.Message}");
                
                if(e is TimeoutException)
                    if(timeoutCanBlockBoot == false)
                        InstallStatus = InstallStatus.TimedOut;
                    else
                        InstallStatus = InstallStatus.Failed;
            }
            
            return InstallStatus;
        }
        
        
        protected abstract UniTask InitializeModule();
    }
}