using System;
using Core.Interfaces.MVC;

namespace Scenes.SceneGame.Models
{
    public class PauseGameModel : IModel
    {
        public float PausePopupDelayAfterContinue { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}