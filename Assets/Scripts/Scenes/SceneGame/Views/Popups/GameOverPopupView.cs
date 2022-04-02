using Common.Enums;
using Core.Popup;
using Core.Statics;
using DG.Tweening;
using Scenes.Common;
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
        private EnergyView energyView;
        [SerializeField]
        private TMP_Text gameOverTitle;
        [SerializeField]
        private TMP_Text notEnoughEnergyText;
        [SerializeField]
        private Button backToMenuButton;
        [SerializeField]
        private Button restartButton;
        [SerializeField]
        private Button buyLifeButton;
        [SerializeField]
        private TMP_Text restartButtonText;
        [SerializeField]
        private TMP_Text backToMenuButtonText;
        [SerializeField]
        private TMP_Text buyLifeButtonText;
        
        private LifesController _lifesController;

        public override void Open()
        {
            OpenAnim();
            ApplyLocalization();
            restartButton.onClick.AddListener(RestartButtonOnClick);
            backToMenuButton.onClick.AddListener(BackToMenuButtonOnClick);
            buyLifeButton.onClick.AddListener(BuyLifeButtonOnClick);
            notEnoughEnergyText.transform.localScale = Vector2.zero;
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
            notEnoughEnergyText.text = Localization.GetFieldText("GameOverNotEnoughEnergyText");
            buyLifeButtonText.text = Localization.GetFieldText("GameOverBuyLifeButtonText");
        }
        
        private void BackToMenuButtonOnClick()
        {
            backToMenuButton.enabled = false;
            transform.DOKill();
            transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).OnComplete(GameOverPopupOnComplete);
        }

        private void GameOverPopupOnComplete()
        {
            AppSceneLoader.Instance.LoadScene(GameScenes.Packs);
        }

        private void RestartButtonOnClick()
        {
            restartButton.enabled = false;
            var currentEnergy = DataRepository.CurrentEnergy;
            currentEnergy--;
            
            if (currentEnergy >= 0)
            {
                DataRepository.CurrentEnergy = currentEnergy;
                _lifesController.RestartLevel();
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

        private void BuyLifeButtonOnClick()
        {
            buyLifeButton.enabled = false;
            var buyLifePopup = AppPopups.Instance.OpenPopup<BuyLifePopupView>();
            buyLifePopup.SetEnergyTimer(energyView);
            buyLifePopup.PopupOnClose = () =>
            {
                buyLifeButton.enabled = true;
            };
        }
    }
}