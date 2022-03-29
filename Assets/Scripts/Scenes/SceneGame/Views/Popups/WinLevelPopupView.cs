using System;
using System.Collections;
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
        private bool _gameIsPassed;
        
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
            _gameIsPassed = false;
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
            var nextLevel = DataRepository.SelectedLevel + 1;
            if (nextLevel < currentPack.Levels.Count)
            {
                packName.text = Localization.GetFieldText(Enum.GetName(typeof(Packs), currentPack.Pack));
                nextLevelButtonText.text = $"{Localization.GetFieldText("WinNextLevel")} {nextLevel + 1}";
            }
            else
            {
                var nextPackId = DataRepository.SelectedPack + 1;
                if (nextPackId < AppConfig.Instance.Packs.Count)
                {
                    var nextPack = AppConfig.Instance.Packs[nextPackId];
                    var nextPackName = Enum.GetName(typeof(Packs), nextPack.Pack);
                    packImage.sprite = nextPack.Image;
                    packName.text = Localization.GetFieldText(nextPackName);
                    nextLevelButtonText.text = Localization.GetFieldText(nextPackName);
                }
                else
                {
                    packName.text = Localization.GetFieldText("WinEndPacks");
                    nextLevelButtonText.text = Localization.GetFieldText("WinBackToMenuEndPacks");
                    _gameIsPassed = true;
                }
            }
            
            backToMenuButtonText.text = Localization.GetFieldText("WinBackToMenu");
        }

        private void BackToMenuButtonOnClick()
        {
            transform.DOKill();
            transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).OnComplete(WinPopupBackToMenuOnComplete);
        }

        private void WinPopupBackToMenuOnComplete()
        {
            AppSceneLoader.Instance.LoadScene(GameScenes.Packs);
        }
        
        private void WinPopupNextLevelOnComplete()
        {
            SceneManager.LoadScene((int)GameScenes.MainMenu);
        }

        private void NextLevelButtonOnClick()
        {
            Close(true);
            
            if (_gameIsPassed)
            {
                transform.DOKill();
                transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).OnComplete(WinPopupNextLevelOnComplete);
            }
            
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
                backToMenuButton.transform.DOKill();
                backToMenuButton.transform.DOScale(Vector2.one, AppConfig.Instance.PopupsConfig.WnPopupButtonsScaleSpeed);
                nextLevelButton.transform.DOKill();
                nextLevelButton.transform.DOScale(Vector2.one, AppConfig.Instance.PopupsConfig.WnPopupButtonsScaleSpeed);
            };
            _levelProgressController.SaveProgress();
        }
    }
}