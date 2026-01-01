using System;
using Cysharp.Threading.Tasks;
using Project.Bootstrap.Enums;
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
            
            m_ServiceInstaller.Install().Forget();
            
            await UniTask.WaitWhile(() => m_ServiceInstaller.InitializeStatus != InitializeStatus.Succeeded);
            Debug.Log($"[{nameof(GameInstaller)}] Install Finished)");
        }

        private void SetProgress(float progress)
        {
            m_ProgressSlider.value = progress;
        }
    }
}