using System.Collections.Generic;
using System.Linq;
using Scripts.Core.Interfaces.MVC;
using Scripts.Helpers;
using Scripts.Scenes.SceneGame.Controllers.Models;
using UnityEngine;

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
            }
        }
    }
}