using System.Collections.Generic;
using System.Linq;
using Core.Interfaces.MVC;
using Core.Statics;
using DG.Tweening;
using Scenes.SceneGame.Controllers;
using Scenes.SceneGame.Models;
using Scenes.SceneGame.Views.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.SceneGame.Views
{
    public class LifesView : MonoBehaviour, IView
    {
        [SerializeField] 
        private GameObject lifePrefab; 
        
        [SerializeField]
        private GridLayoutGroup lifeGridLayoutGroup;
        
        private LifesModel _lifesModel;
        private LifesController _lifesController;
        private readonly Stack<GameObject> _lifesStack = new Stack<GameObject>();
        private const float DefaultGridCellSize = 80f;
        private const int DefaultLifeCountStep = 4;
        
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
                    var lifeGameObject = Instantiate(lifePrefab, lifeGridLayoutGroup.transform);
                    lifeGameObject.transform.localScale = Vector2.zero;
                    lifeGameObject.transform.DOKill();
                    lifeGameObject.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.InBack);
                    _lifesStack.Push(lifeGameObject);
                }
            }
            else if (_lifesStack.Count > _lifesModel.LifesCount)
            {
                while (_lifesStack.Count > _lifesModel.LifesCount)
                {
                    if (_lifesStack.Any())
                    {
                        var deletableLifeObject = _lifesStack.Pop();
                        deletableLifeObject.transform.DOKill();
                        deletableLifeObject.transform.DOScale(Vector2.zero, 0.5f).SetEase(Ease.InBack).onComplete +=
                            () => { Destroy(deletableLifeObject); };
                    }
                }
            }

            if (!_lifesStack.Any())
            {
                AppPopups.Instance.OpenPopup<GameOverPopupView>();
            }

            ResizeLifeGrid();
        }
        
        private void ResizeLifeGrid()
        {
            if (_lifesModel.LifesCount > DefaultLifeCountStep)
            {
                lifeGridLayoutGroup.constraintCount = (int)Mathf.Sqrt(_lifesModel.LifesCount) + DefaultLifeCountStep;
                var size = DefaultGridCellSize / lifeGridLayoutGroup.constraintCount * DefaultLifeCountStep;
                if (size <= DefaultGridCellSize)
                {
                    lifeGridLayoutGroup.cellSize = new Vector2(size, size);
                }
            }
            else
            {
                lifeGridLayoutGroup.cellSize = new Vector2(DefaultGridCellSize, DefaultGridCellSize);
                lifeGridLayoutGroup.constraintCount = _lifesModel.LifesCount;
            }
        }

    }
}