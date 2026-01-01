using Cysharp.Threading.Tasks;
using Project.Bootstrap.Base;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class DummyInstaller : BaseModuleInstaller
    {
        protected override async UniTask InitializeModule()
        {
            Debug.Log($"[{nameof(DummyInstaller)}] InitializeModule()");
            await UniTask.WaitForSeconds(3);
        }
    }
}