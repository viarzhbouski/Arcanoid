using System;
using Core.Interfaces.MVC;
using UnityEngine;

namespace Scenes.SceneGame.Models
{
    public class BallModel : IModel
    {
        public bool IsStarted { get; set; }
        
        public bool BallIsStopped { get; set; }
        
        public Vector2 BallPosition { get; set; }
        
        public float Speed { get; set; }
        
        public float MinBounceAngle { get; set; }
        
        public float ProgressBarStep { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}