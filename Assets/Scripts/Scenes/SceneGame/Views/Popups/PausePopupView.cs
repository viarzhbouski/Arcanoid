using MonoModels;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.SceneGame.Views.Popups
{
    public class PausePopupView : BasePopupView
    {
        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private Button continueButton;

        public Button RestartButton => restartButton;
        
        public Button ContinueButton => continueButton;
    }
}