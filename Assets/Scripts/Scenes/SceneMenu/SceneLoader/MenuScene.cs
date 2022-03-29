using Common.Enums;
using Core.SceneLoader;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.SceneMenu.SceneLoader
{
    public class MenuScene : BaseScene
    {
        [SerializeField]
        private RectTransform logo;
        [SerializeField]
        private RectTransform startButton;
        [SerializeField]
        private RectTransform localizationButton;
        
        private GameScenes _gameScene;

        public override void LoadScene(GameScenes gameScene)
        {
            _gameScene = gameScene;
            logo.DOKill();
            logo.DOLocalMoveX(-sceneCanvasScaler.referenceResolution.x, 0.25f).SetEase(Ease.InBack).OnComplete(LogoOnComplete);
        }

        private void LogoOnComplete()
        {
            startButton.DOKill();
            startButton.DOLocalMoveX(-sceneCanvasScaler.referenceResolution.x, 0.25f).SetEase(Ease.InBack).OnComplete(StartButtonOnComplete);
        }
        
        private void StartButtonOnComplete()
        {
            localizationButton.DOKill();
            localizationButton.DOLocalMoveX(sceneCanvasScaler.referenceResolution.x, 0.25f).SetEase(Ease.InBack).OnComplete(LocalizationButtonOnComplete);
        }
        
        private void LocalizationButtonOnComplete()
        {
            SceneManager.LoadScene((int)_gameScene);
        }
    }
}