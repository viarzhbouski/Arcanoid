﻿using System;
using Scripts.Core.Interfaces.MVC;

namespace Scripts.Scenes.SceneGame.Controllers.Models
{
    public class BallModel : IModel
    {
        public float Speed { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}