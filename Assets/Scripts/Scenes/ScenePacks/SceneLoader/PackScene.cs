using System.Collections;
using Common.Enums;
using Core.SceneLoader;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.ScenePacks.SceneLoader
{
    public class PackScene : BaseScene
    {
        [SerializeField]
        private RectTransform topPanel;
        [SerializeField]
        private RectTransform packs;
        
        private Vector2 _topPanelPosition;
        private Vector2 _packsPosition;
        private GameScenes _gameScene;
        
        public override void LoadScene(GameScenes gameScene)
        {
            _gameScene = gameScene;
            topPanel.DOKill();
            topPanel.DOLocalMoveX(-sceneCanvasScaler.referenceResolution.x, 0.15f).SetEase(Ease.InBack).OnComplete(TopPanelOnComplete);
        }
        
        private void Start()
        {
            _topPanelPosition = topPanel.localPosition;
            _packsPosition = packs.localPosition;
            
            var newTopPanelPosition = new Vector2(_topPanelPosition.x + sceneCanvasScaler.referenceResolution.x, _topPanelPosition.y);
            var newPacksPosition = new Vector2(_packsPosition.x + sceneCanvasScaler.referenceResolution.x, _packsPosition.y);
            
            topPanel.localPosition = newTopPanelPosition;
            packs.localPosition = newPacksPosition;
            
            StartCoroutine(InitWithDelay(newTopPanelPosition, newPacksPosition));
        }

        IEnumerator InitWithDelay(Vector2 newTopPanelPosition, Vector2 newPacksPosition)
        {
            yield return new WaitForSeconds(0.25f);
            InitTopPanel(newTopPanelPosition, newPacksPosition);
        }

        private void InitTopPanel(Vector2 newLogoPosition, Vector2 newPacksPosition)
        {
            topPanel.DOKill();
            topPanel.DOLocalMoveX(newLogoPosition.x - sceneCanvasScaler.referenceResolution.x, 0.15f)
                .SetEase(Ease.InBack).OnComplete(delegate { InitPacksOnComplete(newPacksPosition); });

        }

        private void InitPacksOnComplete(Vector2 newPacksPosition)
        {
            packs.DOKill();
            packs.DOLocalMoveX( newPacksPosition.x - sceneCanvasScaler.referenceResolution.x, 0.15f).SetEase(Ease.InBack);
        }
        
        private void TopPanelOnComplete()
        {
            packs.DOKill();
            packs.DOLocalMoveX( -sceneCanvasScaler.referenceResolution.x, 0.15f).SetEase(Ease.InBack).OnComplete(PacksOnComplete);
        }

        private void PacksOnComplete()
        {
            SceneManager.LoadScene((int)_gameScene);
        }
    }
}