using Common.Enums;
using Core.Popup;
using Core.Statics;
using DG.Tweening;
using Scenes.SceneGame.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

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
            transform.DOKill();
            transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).OnComplete(GameOverPopupOnComplete);
        }

        private void GameOverPopupOnComplete()
        {
            AppSceneLoader.Instance.LoadScene(GameScenes.Packs);
        }

        private void RestartButtonOnClick()
        {
            _lifesController.RestartLevel();
            Close(true);
        }
    }
}