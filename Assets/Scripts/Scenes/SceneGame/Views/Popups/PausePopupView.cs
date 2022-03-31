using System.Collections;
using Common.Enums;
using Core.Popup;
using Core.Statics;
using DG.Tweening;
using Scenes.SceneGame.Controllers;
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
        private Button backToMenuButton;
        [SerializeField]
        private Button continueButton;
        [SerializeField]
        private TMP_Text restartButtonText;
        [SerializeField]
        private TMP_Text backToMenuButtonText;
        [SerializeField]
        private TMP_Text notEnoughEnergyText;
        
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
            notEnoughEnergyText.text = Localization.GetFieldText("PauseNotEnoughEnergyText");
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
            var currentEnergy = DataRepository.CurrentEnergy;
            currentEnergy--;
            
            if (currentEnergy >= 0)
            {
                DataRepository.CurrentEnergy = currentEnergy;
                _pauseGameController.RestartLevel();
                Close(true);
            }
            else if (!notEnoughEnergyText.gameObject.activeSelf)
            {
                notEnoughEnergyText.gameObject.SetActive(true);
                notEnoughEnergyText.transform.DOKill();
                notEnoughEnergyText.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutBounce);
            }
            else
            {
                notEnoughEnergyText.transform.DOKill();
                notEnoughEnergyText.transform.DOPunchScale(Vector2.one * 0.1f, 0.5f).SetEase(Ease.OutBounce).onComplete += () =>
                {
                    notEnoughEnergyText.transform.localScale = Vector2.one;
                };
            }
        }

        IEnumerator ContinueGame()
        {
            Close();
            yield return new WaitForSeconds(AppConfig.Instance.PopupsConfig.PausePopupDelayAfterContinue);
            Destroy(gameObject);
        }
    }
}