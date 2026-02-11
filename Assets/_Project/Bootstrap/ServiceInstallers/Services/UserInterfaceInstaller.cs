using Cysharp.Threading.Tasks;
using Project.Application.Ports.Services;
using Project.Bootstrap.Base;
using Project.Infrastructure.Services;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class UserInterfaceInstaller : BaseServiceInstaller<UserInterfaceService>
    {
        protected override async UniTask InitializeModule()
        {
            Service = gameObject.AddComponent<UserInterfaceService>();
        }
    }
}