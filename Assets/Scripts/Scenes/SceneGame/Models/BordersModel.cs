using System;
using Core.Interfaces.MVC;
using UnityEngine;

namespace Scenes.SceneGame.Models
{
    public class BordersModel : IModel
    {
        public Vector2 TopPanelPosition { get; set; }
        
        public float TopBorderPosition { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}