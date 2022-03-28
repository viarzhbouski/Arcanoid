using System.Collections;
using Common.Enums;
using Core.ObjectPooling;
using Core.Popup;
using Core.Statics;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.ScenePools;
using TMPro;
using UnityEngine;
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
        private Button continueButton;
        
        [SerializeField]
        private TMP_Text restartButtonText;
        
        [SerializeField]
        private TMP_Text continueButtonText;

        private PauseGameController _pauseGameController;
        private PlatformController _platformController;

        public override void Open()
        {
            OpenAnim();
            ApplyLocalization();
            _pauseGameController = AppControllers.Instance.GetController<PauseGameController>();
            _platformController = AppControllers.Instance.GetController<PlatformController>();
            //_platformController.IsStarted(false);
            restartButton.onClick.AddListener(RestartButtonOnClick);
            continueButton.onClick.AddListener(ContinueButtonOnClick);
        }

        protected override void Close(bool destroyAfterClose = false)
        {
            CloseAnim(destroyAfterClose);
        }

        private void ApplyLocalization()
        {
            pauseTitle.text = Localization.GetFieldText(LocaleFields.PauseTitle);
            restartButtonText.text = Localization.GetFieldText(LocaleFields.PauseRestart);
            continueButtonText.text = Localization.GetFieldText(LocaleFields.PauseContinue);
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