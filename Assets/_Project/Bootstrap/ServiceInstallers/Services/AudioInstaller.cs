using Cysharp.Threading.Tasks;
using Project.Bootstrap.Base;
using Project.Infrastructure.Services;
using UnityEngine;
using UnityEngine.Audio;

namespace Project.Bootstrap.ServiceInstallers
{
    public class AudioInstaller : BaseServiceInstaller<AudioService>
    {
        [SerializeField] private AudioMixer _audioMixer;
        
        protected override async UniTask InitializeModule()
        {
            Service = gameObject.AddComponent<AudioService>();
            Service.BindDependencies(_audioMixer);
            await Service.Initialize();
        }
    }
}