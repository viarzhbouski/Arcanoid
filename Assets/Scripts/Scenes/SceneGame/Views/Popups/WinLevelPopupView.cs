using System;
using System.Collections;
using Common.Enums;
using Core.Popup;
using Core.Statics;
using DG.Tweening;
using Scenes.Common;
using Scenes.SceneGame.Controllers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.SceneGame.Views.Popups
{
    public class WinLevelPopupView : BasePopupView
    {
        [SerializeField]
        private EnergyView energyView;
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
        private Text percents;
        [SerializeField]
        private RectTransform progressBar;
        [SerializeField]
        private RectTransform energySprite;
        [SerializeField]
        private ParticleSystem winEffect;
        
        private PackConfig _nextPack;
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
            var progressBarCurrentPositionX = progressBarPositionX;

            percents.text = ((int)(progressBarCurrentPositionX * 100)).ToString();
            progressBar.localScale = new Vector2(progressBarCurrentPositionX, 1f);
            progressBarPositionX += progressBarStep;
            
            StartCoroutine(ShowProgressBar(progressBarCurrentPositionX, progressBarPositionX));
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
                    packName.text = Localization.GetFieldText(nextPackName);
                    nextLevelButtonText.text = Localization.GetFieldText(nextPackName);
                    _nextPack = nextPack;
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
            backToMenuButton.enabled = false;
            DOTween.KillAll();
            transform.DOKill();
            transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InBack).OnComplete(WinPopupBackToMenuOnComplete);
        }

        private void WinPopupBackToMenuOnComplete()
        {
            AppSceneLoader.Instance.LoadScene(GameScenes.Packs);
        }
        
        private void WinPopupNextLevelOnComplete()
        {
            AppSceneLoader.Instance.LoadScene(GameScenes.MainMenu);
        }

        private void NextLevelButtonOnClick()
        {
            nextLevelButton.enabled = false;
            DOTween.KillAll();
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
            _levelProgressController.SaveProgress();
        }
        
        IEnumerator ShowProgressBar(float progressBarCurrentPositionX, float progressBarPositionX)
        {
            yield return new WaitForSeconds(AppConfig.Instance.PopupsConfig.WinPopupProgressBarDelay);
            progressBar.DOKill();
            progressBar.DOScaleX(progressBarPositionX, AppConfig.Instance.PopupsConfig.WinPopupProgressBarSpeed).onComplete += () =>
            {
                StartCoroutine(AddEnergy());

                if (_nextPack != null)
                {
                    winEffect.Play();
                    packImage.transform.DOKill();
                    packImage.transform.DOScale(Vector2.zero, AppConfig.Instance.PopupsConfig.WinPopupImageScaleSpeed).SetEase(Ease.InBounce).onComplete += () =>
                    {
                        packImage.sprite = _nextPack.Image;
                        packImage.transform.DOScale(Vector2.one, AppConfig.Instance.PopupsConfig.WinPopupImageScaleSpeed).SetEase(Ease.OutBounce).onComplete += () =>
                        {
                            backToMenuButton.transform.DOKill();
                            backToMenuButton.transform.DOScale(Vector2.one, AppConfig.Instance.PopupsConfig.WinPopupButtonsScaleSpeed);
                            nextLevelButton.transform.DOKill();
                            nextLevelButton.transform.DOScale(Vector2.one, AppConfig.Instance.PopupsConfig.WinPopupButtonsScaleSpeed);
                        };
                    };
                }
                else
                {
                    if (_gameIsPassed)
                    {
                        winEffect.Play();
                    }
                    
                    backToMenuButton.transform.DOKill();
                    backToMenuButton.transform.DOScale(Vector2.one, AppConfig.Instance.PopupsConfig.WinPopupButtonsScaleSpeed);
                    nextLevelButton.transform.DOKill();
                    nextLevelButton.transform.DOScale(Vector2.one, AppConfig.Instance.PopupsConfig.WinPopupButtonsScaleSpeed);
                }
            };
            
            var percentStart = (int)(progressBarCurrentPositionX * 100);
            var percentEnd = (int)(progressBarPositionX * 100);
            
            percents.DOCounter(percentStart, percentEnd, AppConfig.Instance.PopupsConfig.WinPopupProgressBarSpeed);
        }
        
        IEnumerator AddEnergy()
        {
            for (var i = 0; i < AppConfig.Instance.EnergyConfig.CompleteLevelEnergy; i++)
            {
                yield return new WaitForSeconds(0.1f);
                var sprite = Instantiate(energySprite, transform);
                sprite.DOKill();
                sprite.DOJump(energyView.LogoTransform.position, 1f, 1, 1f).onComplete += () =>
                {
                    energyView.EncreaseEnergy();
                    Destroy(sprite.gameObject);
                };
            }
        }

    }
}