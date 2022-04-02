﻿using System.Collections.Generic;
using Core.Interfaces.MVC;
using Core.Statics;
using Scenes.SceneGame.Models;
using UnityEngine;

namespace Scenes.SceneGame.Views
{
    public class BordersView : MonoBehaviour, IView
    {
        [SerializeField] 
        private Camera gameCamera;
        
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
            var bordersPoints = new List<Vector2>()
            {
                ResizeHelper.ResizePosition(Vector2.zero, gameCamera),
                ResizeHelper.ResizePosition(new Vector2(Vector2.zero.x, _bordersModel.TopBorderPosition), gameCamera),
                ResizeHelper.ResizePosition(new Vector2(Vector2.right.x, _bordersModel.TopBorderPosition), gameCamera),
                ResizeHelper.ResizePosition(Vector2.right, gameCamera),
            };

            bordersCollider.SetPoints(bordersPoints);
        }
    }
}