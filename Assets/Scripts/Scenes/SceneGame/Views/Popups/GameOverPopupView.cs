using Common.Enums;
using Core.Popup;
using Core.Statics;
using Scenes.SceneGame.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private LifesController _lifesController;

        public override void Open()
        {
            OpenAnim();
            ApplyLocalization();
            restartButton.onClick.AddListener(RestartButtonOnClick);
            backToMenuButton.onClick.AddListener(BackToMenuButtonOnClick);
            _lifesController = AppControllers.Instance.GetController<LifesController>();
        }
        
        protected override void Close(bool destroyAfterClose = false)
        {
            CloseAnim(destroyAfterClose);
        }
        
        private void ApplyLocalization()
        {
            gameOverTitle.text = Localization.GetFieldText("GameOverTitle");
            restartButtonText.text = Localization.GetFieldText("GameOverRestart");
            backToMenuButtonText.text = Localization.GetFieldText("GameOverBackToMenu");
        }
        
        private void BackToMenuButtonOnClick()
        {
            SceneManager.LoadScene((int)GameScenes.Packs);
        }

        private void RestartButtonOnClick()
        {
            _lifesController.RestartLevel();
            Close(true);
        }
    }
}