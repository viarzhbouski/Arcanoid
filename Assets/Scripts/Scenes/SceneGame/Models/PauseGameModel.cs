using System;
using Scripts.Core.Interfaces.MVC;

namespace Scripts.Scenes.SceneGame.Controllers.Models
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