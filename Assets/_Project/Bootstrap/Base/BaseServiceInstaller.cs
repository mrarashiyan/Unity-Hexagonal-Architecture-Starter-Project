using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Bootstrap.Enums;
using Project.Bootstrap.Interfaces;
using Project.Presentation.Infrastructures.Base;
using UnityEngine;

namespace Project.Bootstrap.Base
{
    public abstract class BaseServiceInstaller<TService> : MonoBehaviour,IServiceInstaller where TService : BaseService
    {
        public TService Service { get; protected set; }
        public bool TimeoutCanBlockBoot { get; protected set; }
        public InstallStatus InstallStatus { get; protected set; }

        protected IEventBus _eventBus;
        
        public async UniTask<InstallStatus> Initialize(IEventBus eventBus, int timeout = 5, bool timeoutCanBlockBoot = false)
        {
            Debug.Log($"[{GetType().Name}] Initialize: Started");
            _eventBus = eventBus;
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
            
            Debug.Log($"[{GetType().Name}] Initialize: Finished - Result: {InstallStatus}");
            return InstallStatus;
        }
        
        
        protected abstract UniTask InitializeModule();
    }
}