using Cysharp.Threading.Tasks;
using Project.Bootstrap.Base;
using Project.Presentation.Infrastructures.GameTime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Bootstrap.ServiceInstallers
{
    public class TimeInstaller : BaseModuleInstaller
    {
        [SerializeField] private GameTime m_GameTime;
        
        protected override async UniTask InitializeModule()
        {
            await m_GameTime.Initialize();
        }
    }
}