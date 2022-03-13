using System;
using System.Collections.Generic;
using Scripts.Core.Interfaces.MVC;
using UnityEngine;

namespace Scenes.ScenePack.Models
{
    
    public class PacksModel : IModel
    {
        public List<Pack> Packs { get; set; } = new List<Pack>();
        
        public Action OnChange { get; set; }
        
        public void OnChangeHandler(Action action)
        {
            OnChange = action;
        }

        #region Model classes
        
        public class Pack
        {
            public int Id { get; set; }
            
            public Sprite Image { get; set; }
        
            public string Name { get; set; }
            
            public bool CanChoose { get; set; }
        }

        #endregion
    }
}