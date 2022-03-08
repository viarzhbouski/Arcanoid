using System;
using System.Collections.Generic;
using Scripts.Core.Interfaces.MVC;
using Scripts.Core.Models;
using UnityEngine;

namespace Scripts.Scenes.SceneGame.Controllers.Models
{
    public class GenerateLevelModel : IModel
    {
        public Vector2 CellSize { get; set; }
        
        public Vector2 StartPosition { get; set; }

        public List<BlockPosition> Blocks { get; set; } = new List<BlockPosition>();
        
        public Action OnChange { get; set; }

        public void OnChangeHandler(Action onChange)
        {
            OnChange = onChange;
        }
    }
}