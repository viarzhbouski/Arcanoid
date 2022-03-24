using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Core.Interfaces.MVC;
using Core.ObjectPooling;
using Core.Statics;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.ScenePools;
using Scenes.SceneGame.Views.Popups;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.SceneGame.Views
{
    public class LifesView : MonoBehaviour, IView
    {
        [SerializeField] 
        private GameObject lifePrefab; 
        
        [SerializeField]
        private Transform lifeGridUI;
        
        private LifesModel _lifesModel;
        private LifesController _lifesController;
        private GameOverPopupView _gameOverPopupView;
        private readonly Stack<GameObject> _lifesStack = new Stack<GameObject>();

        public void Bind(IModel model, IController controller)
        {
            _lifesModel = model as LifesModel;
            _lifesController = controller as LifesController;
        }

        public void RenderChanges()
        {
            RenderLifes();
        }

        private void RenderLifes()
        {
            if (_lifesModel.IsStartGame)
            {
                while (_lifesStack.Any())
                {
                    Destroy(_lifesStack.Pop());
                }
            }
            
            if (!_lifesStack.Any())
            {
                for (var i = 0; i < _lifesModel.LifesCount; i++)
                {
                    var lifeGameObject = Instantiate(lifePrefab, lifeGridUI);
                    _lifesStack.Push(lifeGameObject);
                }
            }
            else
            {
                Destroy(_lifesStack.Pop());

                if (!_lifesStack.Any())
                {
                    _gameOverPopupView = AppPopups.Instance.ShowPopup<GameOverPopupView>();
                    _gameOverPopupView.RestartButton.onClick.AddListener(GameOverPopupRestartButtonOnClick);
                    _gameOverPopupView.BackToMenuButton.onClick.AddListener(GameOverPopupBackToMenuButtonOnClick);
                }
            }
        }

        private void GameOverPopupBackToMenuButtonOnClick()
        {
            SceneManager.LoadScene((int)GameScenes.Packs);
        }

        private void GameOverPopupRestartButtonOnClick()
        {
            ClearBlockPools();
            AppPopups.Instance.ClosePopup(_gameOverPopupView);
            _lifesController.RestartLevel();
        }

        private void ClearBlockPools()
        {
            ObjectPools.Instance.GetObjectPool<ColorBlockPool>()
                .ClearPool();
            ObjectPools.Instance.GetObjectPool<GraniteBlockPool>()
                .ClearPool();
            ObjectPools.Instance.GetObjectPool<BoostBlockPool>()
                .ClearPool();
        }
    }
}