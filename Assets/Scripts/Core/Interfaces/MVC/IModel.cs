using System;

namespace Scripts.Core.Interfaces.MVC
{
    public interface IModel
    {
        public Action OnChange { get; set; }
        
        public void OnChangeHandler(Action action);
    }
}