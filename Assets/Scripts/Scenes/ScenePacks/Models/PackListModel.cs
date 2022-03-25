using System;
using System.Collections.Generic;
using Core.Interfaces.MVC;
using UnityEngine;

namespace Scenes.ScenePack.Models
{
    
    public class PackListModel : IModel
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
            
            public Sprite PackIcon { get; set; }
        
            public string Name { get; set; }
            
            public int CurrentLevel { get; set; }
            
            public int MaxLevels { get; set; }
            
            public bool CanChoose { get; set; }
        }

        #endregion
    }
}