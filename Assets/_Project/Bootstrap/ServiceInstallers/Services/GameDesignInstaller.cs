using Cysharp.Threading.Tasks;
using Project.Bootstrap.Base;
using Project.Infrastructure.Services;
using UnityEngine;

namespace Project.Bootstrap.ServiceInstallers
{
    public class GameDesignInstaller : BaseServiceInstaller<GameDesignService>
    {
        protected override async UniTask InitializeModule()
        {
            Service = gameObject.AddComponent<GameDesignService>();
        }
    }
}