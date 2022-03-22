using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Managers;
using Scenes.SceneGame.Views.Popups;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class LifesView : MonoBehaviour, IView
    {
        [SerializeField] 
        private GameObject lifePrefab; 
        
        [SerializeField]
        private Transform lifeGridUI;
        
        private LifesModel _lifesModel;
        private readonly Stack<GameObject> _lifesStack = new Stack<GameObject>();

        public void Bind(IModel model, IController controller)
        {
            _lifesModel = model as LifesModel;
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
                    var popup = PopupManager.Instance.ShowPopup<GameOverPopupView>();
                    popup.RestartButton.onClick.AddListener(GameOverPopupRestartButtonOnClick);
                    popup.BackToMenuButton.onClick.AddListener(GameOverPopupBackToMenuButtonOnClick);
                }
            }
        }

        private void GameOverPopupBackToMenuButtonOnClick()
        {
            SceneManager.LoadScene((int)GameScenes.Packs);
        }

        private void GameOverPopupRestartButtonOnClick()
        {
            throw new System.NotImplementedException();
        }
    }
}