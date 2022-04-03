using System;
using Core.Interfaces.MVC;
using UnityEngine;

namespace Scenes.SceneGame.Models
{
    public class PlatformModel : IModel
    {
        public bool IsStarted { get; set; }
        
        public bool IsHold { get; set; }
        
        public Vector2 PlatformBallStartPosition { get; set; }
        
        public float Speed { get; set; }
        
        public float ExtraSpeed { get; set; }

        public float PlatformSpeed => Speed + ExtraSpeed;
        
        public Vector2 StartPosition { get; set; }
        
        public Vector2 Position { get; set; }
        
        public Vector2? TapPosition { get; set; }

        public Action OnChange { get; set; }
        
        public float Size { get; set; }
        
        public float ExtraSize { get; set; }

        public float PlatformSize => Size + ExtraSize;
        
        public bool SizeNeedChange { get; set; }
        
        public bool PlatformOnStart { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}