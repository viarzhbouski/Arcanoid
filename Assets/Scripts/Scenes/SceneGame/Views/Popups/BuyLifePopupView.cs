using Core.Popup;
using Core.Statics;
using DG.Tweening;
using Scenes.Common;
using Scenes.SceneGame.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.SceneGame.Views.Popups
{
    public class BuyLifePopupView : BasePopupView
    {
        [SerializeField]
        private TMP_Text energyInfoText;
        [SerializeField]
        private TMP_Text energyInfoValue;
        [SerializeField]
        private TMP_Text notEnoughEnergyText;
        [SerializeField]
        private Button closePopupButton;
        [SerializeField]
        private Button buyLifeButton;
        [SerializeField]
        private TMP_Text buyLifeButtonText;
        
        private EnergyView _energyView;

        public override void Open()
        {
            energyInfoValue.text = AppConfig.Instance.EnergyConfig.LifeCost.ToString();
            closePopupButton.onClick.AddListener(ClosePopupButtonOnClick);
            buyLifeButton.onClick.AddListener(BuyLifeButtonOnClick);
            notEnoughEnergyText.transform.localScale = Vector2.zero;
            OpenAnim();
            ApplyLocalization();
        }

        public void SetEnergyTimer(EnergyView energyView)
        {
            _energyView = energyView;
        }
        
        public override void Close(bool destroyAfterClose = false)
        {
            CloseAnim(destroyAfterClose);
        }
        
        private void ApplyLocalization()
        {
            energyInfoText.text = Localization.GetFieldText("EnergyInfoText");
            notEnoughEnergyText.text = Localization.GetFieldText("BuyLifeNotEnoughEnergyText");
            buyLifeButtonText.text = Localization.GetFieldText("BuyLifeButtonText");
        }

        private void BuyLifeButtonOnClick()
        {
            buyLifeButton.enabled = false;
            var currentEnergy = _energyView.CurrentEnergy;
            currentEnergy -= AppConfig.Instance.EnergyConfig.LifeCost;

            if (currentEnergy >= 0)
            {
                AppControllers.Instance.GetController<LifesController>()
                    .AddExtraLife();
                _energyView.UseEnergy(AppConfig.Instance.EnergyConfig.LifeCost);
                Close(true);
                AppPopups.Instance.ClosePopup<GameOverPopupView>();
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

        private void ClosePopupButtonOnClick()
        {
            closePopupButton.enabled = false;
            notEnoughEnergyText.gameObject.SetActive(false);
            Close(true);
        }
    }
}