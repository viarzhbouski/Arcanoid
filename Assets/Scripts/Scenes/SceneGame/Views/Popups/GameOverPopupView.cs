using Core.Popup;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.SceneGame.Views.Popups
{
    public class GameOverPopupView : BasePopupView
    {
        [SerializeField]
        private Button backToMenuButton;
        
        [SerializeField]
        private Button restartButton;
        
        public Button BackToMenuButton => backToMenuButton;
        
        public Button RestartButton => restartButton;
    }
}