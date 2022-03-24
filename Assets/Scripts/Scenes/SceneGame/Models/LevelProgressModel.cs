using System;
using Core.Interfaces.MVC;

namespace Scenes.SceneGame.Models
{
    public class LevelProgressModel : IModel
    {
        public bool IsStartGame { get; set; }
        
        public int BlocksAtGameField { get; set; }
        
        public float ProgressBarStep { get; set; }
        
        public float ProgressBarXPosition { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}