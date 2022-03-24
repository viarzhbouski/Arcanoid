using System;
using System.Collections.Generic;
using Scripts.Core.Interfaces.MVC;

namespace Scripts.Core
{
    public class AppContext
    {
        public static AppContext Context;
        private readonly Dictionary<Type, IController> _controllers;

        public AppContext()
        {
            if (Context == null)
            {
                Context = this;
                _controllers = new Dictionary<Type, IController>();
            }
            else
            {
                Context._controllers.Clear();
            }
        }

        public void AddController(IController controller)
        {
            _controllers.Add(controller.GetType(), controller);
        }
        
        public T GetController<T>() where T : IController
        {
            return (T)_controllers[typeof(T)];
        }
    }
}