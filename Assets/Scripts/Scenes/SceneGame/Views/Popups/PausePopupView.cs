using System.Collections;
using Common.Enums;
using Core;
using Core.Popup;
using Core.Statics;
using DG.Tweening;
using Scenes.SceneGame.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes.SceneGame.Views.Popups
{
    public class PausePopupView : BasePopupView
    {
        [SerializeField]
        private TMP_Text pauseTitle;
        
        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private Button backToMenuButton;
        
        [SerializeField]
        private Button continueButton;
        
        [SerializeField]
        private TMP_Text restartButtonText;
        
        [SerializeField]
        private TMP_Text backToMenuButtonText;

        private PauseGameController _pauseGameController;

        public override void Open()
        {
            OpenAnim();
            ApplyLocalization();
            _pauseGameController = AppControllers.Instance.GetController<PauseGameController>();
            restartButton.onClick.AddListener(RestartButtonOnClick);
            backToMenuButton.onClick.AddListener(BackToMenuButtonOnClick);
            continueButton.onClick.AddListener(ContinueButtonOnClick);
        }

        protected override void Close(bool destroyAfterClose = false)
        {
            CloseAnim(destroyAfterClose);
        }

        private void ApplyLocalization()
        {
            pauseTitle.text = Localization.GetFieldText("PauseTitle");
            restartButtonText.text = Localization.GetFieldText("PauseRestart");
            backToMenuButtonText.text = Localization.GetFieldText("PauseBackToMenu");
        }

        private void BackToMenuButtonOnClick()
        {
            transform.DOKill();
            transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).OnComplete(PausePopupOnComplete);
        }

        private void PausePopupOnComplete()
        {
            AppSceneLoader.Instance.LoadScene(GameScenes.Packs);
        }
        
        private void ContinueButtonOnClick()
        {
            StartCoroutine(ContinueGame());
        } 
        
        private void RestartButtonOnClick()
        {
            Close(true);
            _pauseGameController.RestartLevel();
        }

        IEnumerator ContinueGame()
        {
            Close();
            yield return new WaitForSeconds(AppConfig.Instance.PopupsConfig.PausePopupDelayAfterContinue);
            Destroy(gameObject);
        }
    }
}