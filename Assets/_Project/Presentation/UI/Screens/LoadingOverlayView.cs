using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Presentation.UI.Screens.Base;
using UnityEngine;

namespace Project.Presentation.UI.Screens
{
    public class LoadingOverlayView : BaseScreenView
    {
        [SerializeField] private Animator _animator;
        
        public override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
        {
            serviceLocator.UserInterface.SetTransitionOverlay(this);
        }

        public async UniTask TransitIn()
        {
            _animator.Play("In");
            await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        }

        public async UniTask TransitOut()
        {
            _animator.Play("Out");
            await UniTask.WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
        }

        protected override async UniTask BeforeShowScreen()
        {
        }

        protected override async UniTask AfterShowScreen()
        {
            await TransitIn();
        }

        protected override async UniTask BeforeHideScreen()
        {
            await TransitOut();
        }

        protected override async UniTask AfterHideScreen()
        {
        }
    }
}