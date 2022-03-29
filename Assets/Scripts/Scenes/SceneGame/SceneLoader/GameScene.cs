using System.Collections;
using Common.Enums;
using Core.SceneLoader;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Scenes.SceneGame.SceneLoader
{
    public class GameScene : BaseScene
    {
        [SerializeField]
        private RectTransform topPanel;
        [SerializeField]
        private Transform gameField;
        [SerializeField]
        private Transform blocks;
        
        private const float GameFieldOffset = 5;
        private Vector2 _topPanelPosition;
        private Vector2 _gameFieldPosition;
        private Vector2 _blocksPosition;
        private GameScenes _gameScene;
        
        private void Start()
        {
            _topPanelPosition = topPanel.localPosition;
            _gameFieldPosition = gameField.position;
            _blocksPosition = blocks.position;
            
            var newTopPanelPosition = new Vector2(_topPanelPosition.x + sceneCanvasScaler.referenceResolution.x, _topPanelPosition.y);
            var newGameFieldPosition = new Vector2(_gameFieldPosition.x, _gameFieldPosition.y - GameFieldOffset);
            var newBlocksPosition = new Vector2(_blocksPosition.x + sceneCanvasScaler.referenceResolution.x, _blocksPosition.y);
            
            topPanel.localPosition = newTopPanelPosition;
            gameField.position = newGameFieldPosition;
            blocks.localScale = Vector3.forward;

            StartCoroutine(InitWithDelay(newTopPanelPosition, newGameFieldPosition, newBlocksPosition));
        }

        IEnumerator InitWithDelay(Vector2 newTopPanelPosition, Vector2 newGameFieldPosition, Vector2 newBlocksPosition)
        {
            yield return new WaitForSeconds(0.5f);
            InitTopPanel(newTopPanelPosition, newGameFieldPosition, newBlocksPosition);
        }

        private void InitTopPanel(Vector2 newLogoPosition, Vector2 newGameFieldPosition, Vector2 newBlocksPosition)
        {
            topPanel.DOKill();
            topPanel.DOLocalMoveX(newLogoPosition.x - sceneCanvasScaler.referenceResolution.x, 0.25f)
                .SetEase(Ease.InBack).OnComplete(delegate { InitGameFieldOnComplete(newGameFieldPosition, newBlocksPosition); });
        }
        
        private void InitGameFieldOnComplete(Vector2 newGameFieldPosition, Vector2 newBlocksPosition)
        {
            gameField.DOKill();
            gameField.DOMoveY(newGameFieldPosition.y + GameFieldOffset, 0.25f).SetEase(Ease.InBack)
                .OnComplete(delegate { InitBlocksOnComplete(newBlocksPosition); });
        }

        private void InitBlocksOnComplete(Vector2 newBlocksPosition)
        {
            blocks.DOKill();
            blocks.DOScale(Vector3.one, 0.25f).SetEase(Ease.InFlash);
        }
        
        public override void LoadScene(GameScenes gameScene)
        {
            _gameScene = gameScene;
            topPanel.DOKill();
            topPanel.DOLocalMoveX(-sceneCanvasScaler.referenceResolution.x, 0.25f).SetEase(Ease.InBack).OnComplete(TopPanelOnComplete);
        }

        private void TopPanelOnComplete()
        {
            gameField.DOKill();
            gameField.DOLocalMoveY( -GameFieldOffset, 0.25f).SetEase(Ease.InBack).OnComplete(GameFieldOnComplete);
        }
        
        private void GameFieldOnComplete()
        {
            blocks.DOKill();
            blocks.DOScale( Vector3.forward, 0.25f).SetEase(Ease.InBack).OnComplete(BlocksOnComplete);
        }

        private void BlocksOnComplete()
        {
            SceneManager.LoadScene((int)_gameScene);
        }
    }
}