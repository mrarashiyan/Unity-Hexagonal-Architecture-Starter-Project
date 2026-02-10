using System;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.EventBus;
using Project.Application.Ports.ServiceLocator;
using Project.Bootstrap.Enums;
using Project.Infrastructure.ServiceLocator;
using Project.Bootstrap.ServiceInstallers;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Bootstrap
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private ServiceInstaller m_ServiceInstaller;

        [Space]
        [SerializeField] private GameObject m_LoadingCanvas;
        [SerializeField] private Slider m_ProgressSlider;

        private IEventBus _eventBus;
        private IServiceLocator _serviceLocator;
        
        private void OnEnable()
        {
            m_ServiceInstaller.OnProgress += SetProgress;
        }

        private void OnDisable()
        {
            m_ServiceInstaller.OnProgress -= SetProgress;
            
        }

        private async void Start()
        {
            Debug.Log($"[{nameof(GameInstaller)}] Install Started)");
            
            _eventBus = new EventBus();
            _serviceLocator = new ServiceLocator();
            
            m_ServiceInstaller.Install(_eventBus,_serviceLocator).Forget();
            
            await UniTask.WaitWhile(() => m_ServiceInstaller.InstallStatus != InstallStatus.Succeeded);
            Debug.Log($"[{nameof(GameInstaller)}] Install Finished)");
        }

        private void SetProgress(float progress)
        {
            m_ProgressSlider.value = progress;
        }
    }
}