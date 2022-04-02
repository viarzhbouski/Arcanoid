using System.Collections.Generic;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using UnityEngine;

namespace Scenes.SceneGame.Views
{
    public class BordersView : MonoBehaviour, IView
    {
        [SerializeField]
        private EdgeCollider2D bordersCollider;
        
        [SerializeField]
        private RectTransform topPanel;
        
        private BordersModel _bordersModel;

        public void Bind(IModel model, IController controller)
        {
            _bordersModel = model as BordersModel;
            _bordersModel!.TopPanelPosition = topPanel.transform.position;
        }

        public void RenderChanges()
        {
            SetBorders();
        }

        private void SetBorders()
        {
            var bordersPoints = new List<Vector2>
            {
                TransformHelper.ResizePosition(Vector2.zero),
                TransformHelper.ResizePosition(new Vector2(Vector2.zero.x, _bordersModel.TopBorderPosition)),
                TransformHelper.ResizePosition(new Vector2(Vector2.right.x, _bordersModel.TopBorderPosition)),
                TransformHelper.ResizePosition(Vector2.right),
            };

            bordersCollider.SetPoints(bordersPoints);
        }
    }
}