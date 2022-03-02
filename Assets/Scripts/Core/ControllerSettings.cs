using System.Collections.Generic;
using Scripts.Core.Interfaces;
using UnityEngine;

namespace Scripts.Core
{
    public class ControllerSettings
    {
        private List<Component> _components = new List<Component>();
        
        private List<Component> Components { get; }

        public void AddComponent(Component component)
        {
            _components.Add(component);    
        }
    }
}