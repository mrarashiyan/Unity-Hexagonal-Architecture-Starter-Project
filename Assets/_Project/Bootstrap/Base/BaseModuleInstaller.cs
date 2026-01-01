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
        public InitializeStatus InitializeStatus { get; protected set; }

        public async UniTask<InitializeStatus> Initialize()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(InitializeTimeout);

            InitializeStatus = InitializeStatus.InProgress;
            try
            {
                await InitializeModule();
                InitializeStatus = InitializeStatus.Succeeded;
            }
            catch (Exception e)
            {
                Debug.LogError($"[ModuleInstallerFailed] Exception={e.Message}");
                InitializeStatus = InitializeStatus.Failed;
            }
            
            return InitializeStatus;
        }
        
        
        protected abstract UniTask InitializeModule();
    }
}