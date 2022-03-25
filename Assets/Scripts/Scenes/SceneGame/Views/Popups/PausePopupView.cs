using Common.Enums;
using Core.Popup;
using Core.Statics;
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

        public Button RestartButton => restartButton;
        
        public Button ContinueButton => continueButton;

        public void Init()
        {
            ApplyLocalization();
        }
        
        private void ApplyLocalization()
        {
            pauseTitle.text = Localization.GetFieldText(LocaleFields.PauseTitle);
            restartButtonText.text = Localization.GetFieldText(LocaleFields.PauseRestart);
            continueButtonText.text = Localization.GetFieldText(LocaleFields.PauseContinue);
        }
    }
}