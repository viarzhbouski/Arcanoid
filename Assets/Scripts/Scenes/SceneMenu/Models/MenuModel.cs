using System;
using Core.Interfaces.MVC;

namespace Scenes.SceneMenu.Models
{
    
    public class MenuModel : IModel
    {
        public Action OnChange { get; set; }
        
        public void OnChangeHandler(Action action)
        {
            OnChange = action;
        }
    }
}