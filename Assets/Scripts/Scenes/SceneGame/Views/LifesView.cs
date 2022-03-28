using System.Collections.Generic;
using System.Linq;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views.Popups;
using UnityEngine;

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
            
            if (_lifesStack.Count < _lifesModel.LifesCount)
            {
                for (var i = _lifesStack.Count; i < _lifesModel.LifesCount; i++)
                {
                    var lifeGameObject = Instantiate(lifePrefab, lifeGridUI);
                    _lifesStack.Push(lifeGameObject);
                }
            }
            else if (_lifesStack.Count > _lifesModel.LifesCount)
            {
                while (_lifesStack.Count > _lifesModel.LifesCount)
                {
                    if (_lifesStack.Any())
                    {
                        Destroy(_lifesStack.Pop());
                    }
                }
            }

            if (!_lifesStack.Any())
            {
                AppPopups.Instance.OpenPopup<GameOverPopupView>();
            }
        }
    }
}