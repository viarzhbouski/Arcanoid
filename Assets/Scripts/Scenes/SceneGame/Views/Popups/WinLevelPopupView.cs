using System;
using System.Collections;
using Common.Enums;
using Core.Popup;
using Core.Statics;
using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.SceneGame.Views.Popups
{
    public class WinLevelPopupView : BasePopupView
    {
        [SerializeField]
        private Image packImage;
        
        [SerializeField]
        private Button nextLevelButton;
        
        [SerializeField]
        private TMP_Text nextLevelButtonText;
        
        [SerializeField]
        private Button backToMenuButton;
        
        [SerializeField]
        private TMP_Text backToMenuButtonText;
        
        [SerializeField]
        private TMP_Text packName;

        [SerializeField]
        private RectTransform progressBar;

        private const float ProgressBarDelay = 0.1f;

        public Button NextLevelButton => nextLevelButton;
        
        public Button BackToMenuButton => backToMenuButton;

        public void Init(PackConfig currentPack)
        {
            ApplyLocalization(currentPack);
            var progressBarScale = progressBar.localScale;
            
            progressBar.localScale = new Vector2(0f, progressBarScale.y);
            packImage.sprite = currentPack.Image;

            var currentLevel = DataRepository.SelectedLevel;
            var progressBarStep = 1f / currentPack.Levels.Count;
            var progressBarPositionX = 0f;
            
            for (var i = 0; i <= currentLevel; i++)
            {
                progressBarPositionX += progressBarStep;
            }

            StartCoroutine(ShowProgressBar(progressBarPositionX));
        }

        private void ApplyLocalization(PackConfig currentPack)
        {
            packName.text = Localization.GetFieldText(currentPack.LocaleField);
            nextLevelButtonText.text = Localization.GetFieldText(LocaleFields.WinNextLevel);
            backToMenuButtonText.text = Localization.GetFieldText(LocaleFields.WinBackToMenu);
        }

        IEnumerator ShowProgressBar(float progressBarPositionX)
        {
            yield return new WaitForSeconds(ProgressBarDelay);
            progressBar.DOKill();
            progressBar.DOScaleX(progressBarPositionX, 0.1f);
        }
    }
}