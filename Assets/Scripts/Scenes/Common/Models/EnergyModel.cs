using System;
using Core.Interfaces.MVC;

namespace Scenes.Common.Models
{
    public class EnergyModel : IModel
    {
        public int Energy { get; set; }
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}