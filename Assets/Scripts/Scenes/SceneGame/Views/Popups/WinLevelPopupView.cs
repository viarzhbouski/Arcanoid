﻿using System.Collections;
using Common.Enums;
using Core.Popup;
using Core.Statics;
using DG.Tweening;
using Scenes.SceneGame.Controllers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private LevelProgressController _levelProgressController;

        public override void Open()
        {
            var currentPackId = DataRepository.SelectedPack;
            var currentPack = AppConfig.Instance.Packs[currentPackId];
            transform.localScale = Vector3.zero;
            nextLevelButton.transform.localScale = Vector3.zero;
            backToMenuButton.transform.localScale = Vector3.zero;
            
            nextLevelButton.onClick.AddListener(NextLevelButtonOnClick);
            backToMenuButton.onClick.AddListener(BackToMenuButtonOnClick);
            _levelProgressController = AppControllers.Instance.GetController<LevelProgressController>();
            ApplyLocalization(currentPack);
            StartCoroutine(OpenWithDelay(currentPack));
        }

        protected override void Close(bool destroyAfterClose = false)
        {
            CloseAnim(destroyAfterClose);
        }

        private void InitPackProgressBar(PackConfig currentPack)
        {
            var progressBarScale = progressBar.localScale;
            
            progressBar.localScale = new Vector2(0f, progressBarScale.y);
            packImage.sprite = currentPack.Image;
            
            var currentLevel = DataRepository.SelectedLevel;
            var progressBarStep = 1f / currentPack.Levels.Count;
            var progressBarPositionX = currentLevel * progressBarStep;
            
            progressBar.localScale = new Vector2(progressBarPositionX, 1f);
            progressBarPositionX += progressBarStep;
            
            StartCoroutine(ShowProgressBar(progressBarPositionX));
        }

        private void ApplyLocalization(PackConfig currentPack)
        {
            packName.text = Localization.GetFieldText(currentPack.LocaleField);
            nextLevelButtonText.text = Localization.GetFieldText(LocaleFields.WinNextLevel);
            backToMenuButtonText.text = Localization.GetFieldText(LocaleFields.WinBackToMenu);
        }

        private void BackToMenuButtonOnClick()
        {
            SceneManager.LoadScene((int)GameScenes.Packs);
        }
        
        private void NextLevelButtonOnClick()
        {
            Close(true);
            _levelProgressController.NextLevel();
        }

        IEnumerator OpenWithDelay(PackConfig currentPack)
        {
            yield return new WaitForSeconds(AppConfig.Instance.PopupsConfig.WinPopupDelay);
            OpenAnim();
            InitPackProgressBar(currentPack);
        }
        
        IEnumerator ShowProgressBar(float progressBarPositionX)
        {
            yield return new WaitForSeconds(AppConfig.Instance.PopupsConfig.WinPopupProgressBarDelay);
            progressBar.DOKill();
            progressBar.DOScaleX(progressBarPositionX, AppConfig.Instance.PopupsConfig.WinPopupProgressBarSpeed).onComplete += () =>
            {
                backToMenuButton.transform.DOScale(Vector2.one, AppConfig.Instance.PopupsConfig.WnPopupButtonsScaleSpeed);
                nextLevelButton.transform.DOScale(Vector2.one, AppConfig.Instance.PopupsConfig.WnPopupButtonsScaleSpeed);
            };
            _levelProgressController.SaveProgress();
        }
    }
}