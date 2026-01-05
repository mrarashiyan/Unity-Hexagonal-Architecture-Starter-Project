using System;
using Cysharp.Threading.Tasks;
using Project.Bootstrap.Base;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class DummyInstaller : BaseModuleInstaller
    {
        [SerializeField] private bool m_SimulateException;
        protected override async UniTask InitializeModule()
        {
            Debug.Log($"[{nameof(DummyInstaller)}] InitializeModule()");
            
            if(m_SimulateException)
                throw new Exception("SimulateException");
            
            await UniTask.WaitForSeconds(3);
        }
    }
}