using System;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Bootstrap.Enums;
using Project.Bootstrap.Interfaces;
using Project.Presentation.UI.Screens.Base;
using UnityEngine;

namespace Project.Bootstrap.Base
{
    public abstract class BaseScreenInstaller<TScreenView> : MonoBehaviour, IScreenInstaller
        where TScreenView : BaseScreenView
    {
        public TScreenView Screen => _screenView;
        public InstallStatus InstallStatus { get; protected set; }

        [SerializeField] private TScreenView _screenView;


        public async UniTask<InstallStatus> Initialize(IEventBus eventBus, IServiceLocator serviceLocator,
            bool markAsDefaultScreen = false)
        {
            Debug.Log($"[{GetType().Name}] Initialize: Started");

            InstallStatus = InstallStatus.InProgress;
            try
            {
                await InitializeScreen(eventBus, serviceLocator);
                InstallStatus = InstallStatus.Succeeded;
            }
            catch (Exception e)
            {
                Debug.LogError($"[{GetType().Name}] Exception={e.Message} \nStack={e.StackTrace}");
                InstallStatus = InstallStatus.Failed;
            }

            serviceLocator.UserInterface.AddScreen(_screenView);
            if (markAsDefaultScreen)
                serviceLocator.UserInterface.SetDefaultScreen(_screenView.GetType());

            Debug.Log($"[{GetType().Name}] Initialize: Finished - Result: {InstallStatus}");
            return InstallStatus;
        }


        protected abstract UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator);
    }
}