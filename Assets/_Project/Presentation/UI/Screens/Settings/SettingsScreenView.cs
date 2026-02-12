using System;
using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Presentation.UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Presentation.UI.Screens.Settings
{
    public class SettingsScreenView : BaseScreenView, ISettingsScreenView
    {
        [SerializeField] private Button _backBtn;

        private SettingsScreenPresenter _presenter;

        private void OnEnable()
        {
            _backBtn.onClick.AddListener(OnBackBtnPressed);
        }

        private void OnDisable()
        {
            _backBtn.onClick.RemoveListener(OnBackBtnPressed);
        }

        public void BindDependencies(SettingsScreenPresenter presenter)
        {
            _presenter = presenter;
        }

        public override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
        {
        }

        protected override async UniTask BeforeShowScreen()
        {
        }

        protected override async UniTask AfterShowScreen()
        {
        }

        protected override async UniTask BeforeHideScreen()
        {
        }

        protected override async UniTask AfterHideScreen()
        {
        }

        private void OnBackBtnPressed()
        {
            _presenter.NavigateToBack();
        }
    }
}