using System;
using Core.Interfaces.MVC;

namespace Scenes.SceneGame.Models
{
    public class LifesModel : IModel
    {
        public bool IsStartGame { get; set; }
        
        public int LifesCount { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}