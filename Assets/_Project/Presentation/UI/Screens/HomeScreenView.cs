using Cysharp.Threading.Tasks;
using Project.Application;
using Project.Application.Ports.ServiceLocator;
using Project.Presentation._Project.Presentation.UI.Widgets;
using Project.Presentation.UI.Screens.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Presentation.UI.Screens
{
    public class HomeScreenView : BaseScreenView
    {
        [SerializeField] private LevelSelectButtonView _levelSelectButtonPrefab;
        [SerializeField] private Transform _levelContainer;
        [SerializeField] private Sprite[] _levelImages;
        
        
        private int _levelsCount;
        private IServiceLocator _serviceLocator;
        
        public override async UniTask InitializeScreen(IEventBus eventBus, IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            _levelsCount = serviceLocator.GameDesignService.LevelData.LevelCount;
        }

        protected override async UniTask BeforeShowScreen()
        {
            for (int i = 0; i < _levelsCount; i++)
            {
                var btn = Instantiate(_levelSelectButtonPrefab, _levelContainer);
                var buttonView = btn.GetComponent<LevelSelectButtonView>();
                buttonView.SetBackground(_levelImages[i]);
                buttonView.levelIndex = i;
                buttonView.OnButtonClick += LevelSelectButtonClicked;
            }
        }

        protected override async UniTask AfterHideScreen()
        {
            while (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
                await UniTask.Yield();
            }
        }

        private void LevelSelectButtonClicked(int levelIndex)
        {
            _serviceLocator.Console.Log(levelIndex.ToString());
        }
    }
}