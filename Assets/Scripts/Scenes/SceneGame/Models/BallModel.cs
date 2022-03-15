using System;
using Scripts.Core.Interfaces.MVC;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Models
{
    public class BallModel : IModel
    {
        public bool IsStarted { get; set; }
        
        public Vector2 BallPosition { get; set; }
        
        public float Speed { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}