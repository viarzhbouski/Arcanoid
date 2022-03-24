using System;
using System.Collections.Generic;
using Core.Interfaces.MVC;

namespace Core.Statics
{
    public class AppControllers
    {
        public static AppControllers Instance;
        private readonly Dictionary<Type, IController> _controllers;

        public AppControllers()
        {
            if (Instance == null)
            {
                Instance = this;
                _controllers = new Dictionary<Type, IController>();
            }
            else
            {
                Instance._controllers.Clear();
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