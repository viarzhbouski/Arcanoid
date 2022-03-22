using System;
using Scripts.Core.Interfaces.MVC;

namespace Scripts.Scenes.SceneGame.Controllers.Models
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