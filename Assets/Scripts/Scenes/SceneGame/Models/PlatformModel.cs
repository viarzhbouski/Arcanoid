using System;
using Scripts.Core.Interfaces.MVC;
using Scripts.Scenes.SceneGame.Controllers.Views;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Models
{
    public class PlatformModel : IModel
    {
        public float PlatformSpeed { get; set; }
        
        public Vector2 Position { get; set; }

        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}