using System;
using Cysharp.Threading.Tasks;
using Project.Bootstrap.Base;
using Project.Infrastructure;
using Project.Infrastructure.Services;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class DummyInstaller : BaseServiceInstaller<DummyService>
    {
        [SerializeField] private bool m_SimulateException;
        protected override async UniTask InitializeModule()
        {
            Debug.Log($"[{nameof(DummyInstaller)}] InitializeModule()");
            Service = gameObject.AddComponent<DummyService>();
            
            if(m_SimulateException)
                throw new Exception("SimulateException");
            
            await UniTask.WaitForSeconds(3);
        }
    }
}