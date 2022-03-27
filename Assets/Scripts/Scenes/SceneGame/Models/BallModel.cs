using System;
using Core.Interfaces.MVC;
using UnityEngine;

namespace Scenes.SceneGame.Models
{
    public class BallModel : IModel
    {
        public bool BallIsPushed { get; set; }
        
        public bool IsStarted { get; set; }
        
        public bool BallIsStopped { get; set; }
        
        public Vector2 BallPosition { get; set; }

        public float Speed { get; set; }
        
        public float ExtraSpeed { get; set; }

        public float BallSpeed => Speed + ExtraSpeed;
        
        public float MinBounceAngle { get; set; }
        
        public float ProgressBarStep { get; set; }
        
        public bool BallCanDestroyAllBlocks { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}