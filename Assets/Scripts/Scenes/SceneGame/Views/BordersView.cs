using System.Collections.Generic;
using Scripts.Core.Interfaces.MVC;
using Scripts.Helpers;
using Scripts.Scenes.SceneGame.Controllers.Models;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Views
{
    public class BordersView : MonoBehaviour, IView
    {
        [SerializeField] 
        private Camera camera;
        
        [SerializeField]
        private EdgeCollider2D bordersCollider;
        
        [SerializeField]
        private EdgeCollider2D bottomBorderCollider;
        
        private BordersModel _bordersModel;

        public void Bind(IModel model, IController controller)
        {
            _bordersModel = model as BordersModel;
        }

        public void RenderChanges()
        {
            SetBorders();
        }

        private void SetBorders()
        {
            var bordersPoints = new List<Vector2>()
            {
                ResizeHelper.ResizePosition(0, 0, camera),
                ResizeHelper.ResizePosition(0, 0.885f, camera),
                ResizeHelper.ResizePosition(1, 0.885f, camera),
                ResizeHelper.ResizePosition(1, 0, camera),
            };
            
            var bottomBordersPoints = new List<Vector2>()
            {
                ResizeHelper.ResizePosition(0, 0, camera),
                ResizeHelper.ResizePosition(1, 0, camera),
            };

            bordersCollider.SetPoints(bordersPoints);
            bottomBorderCollider.SetPoints(bottomBordersPoints);
        }
    }
}