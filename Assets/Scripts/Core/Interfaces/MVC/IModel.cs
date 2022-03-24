using System;

namespace Core.Interfaces.MVC
{
    public interface IModel
    {
        public Action OnChange { get; set; }
        
        public void OnChangeHandler(Action action);
    }
}