using System.Collections;
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
        private Button backToMenuButton;
        
        [SerializeField]
        private TMP_Text packName;

        [SerializeField]
        private RectTransform progressBar;

        private const float ProgressBarDelay = 0.1f;

        public Button NextLevelButton => nextLevelButton;
        
        public Button BackToMenuButton => backToMenuButton;

        public void Init(Pack currentPack)
        {
            var progressBarScale = progressBar.localScale;
            
            progressBar.localScale = new Vector2(0f, progressBarScale.y);
            packName.text = currentPack.Name;
            packImage.sprite = currentPack.Image;
            
            var currentLevel = GameProgress.GetLastLevel();
            var progressBarStep = 1f / currentPack.Levels.Length;
            var progressBarPositionX = 0f;
            
            for (var i = 0; i <= currentLevel; i++)
            {
                progressBarPositionX += progressBarStep;
            }

            StartCoroutine(ShowProgressBar(progressBarPositionX));
        }

        IEnumerator ShowProgressBar(float progressBarPositionX)
        {
            yield return new WaitForSeconds(ProgressBarDelay);
            progressBar.DOKill();
            progressBar.DOScaleX(progressBarPositionX, 0.1f);
        }
    }
}