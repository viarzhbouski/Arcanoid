using System;
using Core.Interfaces.MVC;
using ScriptableObjects;

namespace Scenes.SceneGame.Models
{
    public class LevelProgressModel : IModel
    {
        public bool IsStartGame { get; set; }
        
        public int BlocksAtGameField { get; set; }
        
        public float LevelProgressBarStep { get; set; }
        
        public float LevelProgressBarXPosition { get; set; }
        
        public PackConfig CurrentPack { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}