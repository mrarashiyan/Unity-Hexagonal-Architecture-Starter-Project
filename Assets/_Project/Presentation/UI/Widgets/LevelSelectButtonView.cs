using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Presentation._Project.Presentation.UI.Widgets
{
    public class LevelSelectButtonView : MonoBehaviour
    {
        public Action<int> OnButtonClick;
        public int levelIndex;

        [SerializeField] private Button _button;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(() => OnButtonClick?.Invoke(levelIndex));
        }

        public void SetBackground(Sprite img)
        {
            _button.image.sprite = img;
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}