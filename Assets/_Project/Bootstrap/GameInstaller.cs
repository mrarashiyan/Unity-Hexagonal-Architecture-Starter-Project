using System;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.EventBus;
using Project.Application.Ports.ServiceLocator;
using Project.Bootstrap.Enums;
using Project.Bootstrap.ScreenInstallers;
using Project.Infrastructure.ServiceLocator;
using Project.Bootstrap.ServiceInstallers;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Bootstrap
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private ServiceInstaller m_ServiceInstaller;
        [SerializeField] private ScreenInstaller m_ScreenInstaller;

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

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            Initialize().Forget();
        }

        private async UniTaskVoid Initialize()
        {
            Debug.Log($"[{nameof(GameInstaller)}] Install Started)");
            
            _eventBus = new EventBus();
            _serviceLocator = new ServiceLocator();
            
            m_ServiceInstaller.Install(_eventBus,_serviceLocator).Forget();
            m_ScreenInstaller.Install(_eventBus, _serviceLocator).Forget();
            
            await UniTask.WaitWhile(() => m_ServiceInstaller.InstallStatus != InstallStatus.Succeeded);
            await UniTask.WaitWhile(() => m_ScreenInstaller.InstallStatus != InstallStatus.Succeeded);
            Debug.Log($"[{nameof(GameInstaller)}] Install Finished");
            
            await PostProcess();
        }

        private void SetProgress(float progress)
        {
            m_ProgressSlider.value = progress;
        }

        private async UniTask PostProcess()
        {
            await _serviceLocator.UserInterface.TransitionIn();
            await _serviceLocator.UserInterface.ShowDefaultScreen();
            await _serviceLocator.UserInterface.TransitionOut();
        }
    }
}