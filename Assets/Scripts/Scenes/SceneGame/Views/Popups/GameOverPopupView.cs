using Common.Enums;
using Core.Popup;
using Core.Statics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.SceneGame.Views.Popups
{
    public class GameOverPopupView : BasePopupView
    {
        [SerializeField]
        private TMP_Text gameOverTitle;
        
        [SerializeField]
        private Button backToMenuButton;
        
        [SerializeField]
        private Button restartButton;
        
        [SerializeField]
        private TMP_Text restartButtonText;
        
        [SerializeField]
        private TMP_Text backToMenuButtonText;
        
        public Button BackToMenuButton => backToMenuButton;
        
        public Button RestartButton => restartButton;
        
        // public void Init()
        // {
        //     //ApplyLocalization();
        // }
        
        // private void ApplyLocalization()
        // {
        //     gameOverTitle.text = Localization.GetFieldText(LocaleFields.GameOverTitle);
        //     restartButtonText.text = Localization.GetFieldText(LocaleFields.GameOverRestart);
        //     backToMenuButtonText.text = Localization.GetFieldText(LocaleFields.GameOverBackToMenu);
        // }
        public override void Open()
        {
            throw new System.NotImplementedException();
        }

        protected override void Close(bool destroyAfterClose = false)
        {
            throw new System.NotImplementedException();
        }
    }
}